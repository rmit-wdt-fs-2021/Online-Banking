using InternetBanking.Interfaces;
using InternetBanking.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InternetBanking.BackgroundServices
{
    public class AcitivityReportBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<AcitivityReportBackgroundService> _logger;

        private IAccountService _accountService;

        public AcitivityReportBackgroundService(IServiceProvider services, ILogger<AcitivityReportBackgroundService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            _accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
            List<Account> initialAccounts = await _accountService.GetAllAccountsAsync();

            const int waitTime = 1;
            while (!cancellationToken.IsCancellationRequested)
            {
                await DoWork(cancellationToken, initialAccounts);

                _logger.LogInformation("Activity report service is waiting 3 minutes");

                await Task.Delay(TimeSpan.FromMinutes(waitTime), cancellationToken);
            }
        }

        private async Task DoWork(CancellationToken cancellationToken, List<Account> initialAccounts)
        {
            _logger.LogInformation("Activity report service is working");
            using var scope = _services.CreateScope();
            _accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
            var newAccounts = await _accountService.GetAllAccountsAsync();

            foreach (var initialAccount in initialAccounts)
            {
                var newAccount = newAccounts.FirstOrDefault(x => x.AccountNumber == initialAccount.AccountNumber);
                if(newAccount.ModifyDate != initialAccount.ModifyDate)
                {
                    _logger.LogInformation($"Account : {newAccount.AccountNumber} has been updated...sending email.");
                }
            }
        }

    }
}
