using InternetBanking.Data;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InternetBanking.BackgroundServices
{
    public class BillPayBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private IAccountService _accountService;
        private ITransactionService _transactionService;
        private readonly ILogger<BillPayBackgroundService> _logger;

        public BillPayBackgroundService(IServiceProvider services, ILogger<BillPayBackgroundService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Bill pay service is running");
            const int waitTime = 1;
            while (!cancellationToken.IsCancellationRequested)
            {
                await DoWork(cancellationToken);

                _logger.LogInformation("Bill pay service is waiting a minute");

                await Task.Delay(TimeSpan.FromMinutes(waitTime), cancellationToken);
            }
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Bill pay service is working");

            using var scope = _services.CreateScope();
            McbaContext context = scope.ServiceProvider.GetRequiredService<McbaContext>();
            _transactionService = scope.ServiceProvider.GetRequiredService<ITransactionService>();
            _accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();

            var billPays = await context.BillPay.ToListAsync(cancellationToken);
            foreach (var billPay in billPays)
            {
                if (IsTimeToProcessBill(billPay.ScheduledDate))
                {
                    await ProcessBillAsync(billPay, context).ConfigureAwait(false);
                }
            }

        }

        private async Task ProcessBillAsync(BillPay billPay, McbaContext context)
        {
            var accountToBeCharged = await _accountService.GetAccountAsync(billPay.AccountNumber);

            if (accountToBeCharged is null)
            {
                _logger.LogError($"Unable to find account with {billPay.AccountNumber}");
                return;
            }

            if (ValidateBillAmount(accountToBeCharged, billPay.Amount))
            {
                await _transactionService.AddBillPayTransaction(accountToBeCharged, billPay.Amount).ConfigureAwait(false);
                _logger.LogInformation("Bill Pay service is work complete.");
                await UpdateBillPayAsync(billPay, context).ConfigureAwait(false);
            }
            else
            {
                _logger.LogInformation($"Customer does not have enough balance in Account : {accountToBeCharged.AccountNumber}");
                return;
            }
        }

        private async Task UpdateBillPayAsync(BillPay billPay, McbaContext context)
        {
            if (billPay.Period == BillPeriod.OnceOff)
            {
                context.BillPay.Remove(billPay);
                await context.SaveChangesAsync();
                _logger.LogInformation($"Bill pay : {billPay.BillPayID} has been processed and deleted.");
            }
        }

        private bool IsTimeToProcessBill(DateTime ScheduledDate) => DateTime.UtcNow.Date >= ScheduledDate.Date;

        private bool ValidateBillAmount(Account account, decimal amount)
        {
            const int minCheckingBalance = 200;
            const int minSavingsBalance = 0;
            bool retVal = true;
            if (account.AccountType == AccountType.Checking)
            {
                if (account.Balance - amount < minCheckingBalance)
                {
                    retVal = false;
                }
            }
            else if (account.AccountType == AccountType.Saving)
            {
                if (account.Balance - amount < minSavingsBalance)
                {
                    retVal = false;
                }
            }

            if (amount < 0)
            {
                retVal = false;
            }

            return retVal;
        }
    }
}
