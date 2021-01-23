using InternetBanking.Data;
using InternetBanking.Exceptions;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using InternetBanking.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly McbaContext _context;
        private const int freeTransactions = 4;

        // TODO : Add logger
        public TransactionService(McbaContext context)
        {
            _context = context;
        }

        public async Task AddDepositTransactionAsync(Account account, decimal amount)
        {
            if (account is null)
            {
                throw new ArgumentNullException($"{nameof(account)} cannot be null.");
            }

            account.Balance += amount;
            AddTransaction(account, TransactionType.Deposit, amount);

            await _context.SaveChangesAsync();
        }

        public async Task AddBillPayTransaction(Account account, decimal amount)
        {
            if (account is null)
            {
                throw new ArgumentNullException($"{nameof(account)} cannot be null.");
            }

            if (!IsValidDeductionAmount(account, amount))
            {
                throw new AccountBalanceUpdateException($"Unable to update account by {nameof(amount)}");
            }

            account.Balance -= amount;
            AddTransaction(account, TransactionType.BillPay, amount);

            await _context.SaveChangesAsync();
        }

        public async Task AddWithdrawTransactionAsync(Account account, decimal amount)
        {

            if (account is null)
            {
                throw new ArgumentNullException($"{nameof(account)} cannot be null.");
            }

            if (!IsValidDeductionAmount(account, amount))
            {
                throw new AccountBalanceUpdateException($"Unable to update account by {nameof(amount)}");
            }

            account.Balance -= amount;
            AddTransaction(account, TransactionType.Withdraw, amount);

            if (!account.HasFreeTransactions(freeTransactions))
            {
                AddServiceChargeTransaction(account, TransactionType.Withdraw);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddTransferTransactionAsync(Account srcAccount, Account destAccount, decimal amount, string comment = null)
        {
            if (srcAccount is null || destAccount is null)
            {
                throw new NullReferenceException($"Unable to find account.");
            }

            if(srcAccount.AccountNumber == destAccount.AccountNumber)
            {
                throw new ArgumentException($"{nameof(srcAccount)} and {nameof(destAccount)} accounts cannot be same.");
            }

            if (!IsValidDeductionAmount(srcAccount, amount))
            {
                throw new AccountBalanceUpdateException($"Unable to update account by {nameof(amount)}");
            }

            UpdateTransferAccountBalances(srcAccount, destAccount, amount, comment);

            if (!srcAccount.HasFreeTransactions(freeTransactions))
            {
                AddServiceChargeTransaction(srcAccount, TransactionType.Transfer);
            }

            await _context.SaveChangesAsync();
        }

        private void AddServiceChargeTransaction(Account account, TransactionType type)
        {
            decimal serviceCharge;
            if (type == TransactionType.Transfer)
            {
                serviceCharge = 0.20M;
            }
            else if (type == TransactionType.Withdraw)
            {
                serviceCharge = 0.10M;
            }
            else
            {
                throw new InvalidOperationException($"Unable to apply service charge for {nameof(type)}");
            }

            if (!IsValidDeductionAmount(account, serviceCharge))
            {
                throw new AccountBalanceUpdateException($"Unable to update account by {nameof(serviceCharge)}");
            }

            account.Balance -= serviceCharge;
            AddTransaction(account, TransactionType.ServiceCharge, serviceCharge);
        }

        private void UpdateTransferAccountBalances(Account srcAccount, Account destAccount, decimal amount, string comment = null)
        {
            srcAccount.Balance -= amount;
            AddTransaction(srcAccount, TransactionType.Transfer, amount, destAccount.AccountNumber, comment);

            destAccount.Balance += amount;
            destAccount.ModifyDate = DateTime.UtcNow;
        }

        private void AddTransaction(Account account, TransactionType type, decimal amt, int? destNumber = null, string comment = null)
        {
            account.Transactions.Add(
                new Transaction
                {
                    TransactionType = type,
                    AccountNumber = account.AccountNumber,
                    Amount = amt,
                    DestinationAccountNumber = destNumber,
                    Comment = comment,
                    TransactionTimeUtc = DateTime.UtcNow
                });
            account.ModifyDate = DateTime.UtcNow;
        }

        private bool IsValidDeductionAmount(Account account, decimal amount)
        {
            bool retVal = true;
            const int checkingAccMinBal = 200;
            const int savingAccMinBal = 0;

            if (account.AccountType == AccountType.Checking)
            {
                if (account.Balance - amount < checkingAccMinBal)
                {
                    retVal = false;
                }
            }
            else if (account.AccountType == AccountType.Saving)
            {
                if (account.Balance - amount < savingAccMinBal)
                {
                    retVal = false;
                }
            }

            return retVal;
        }
    }
}
