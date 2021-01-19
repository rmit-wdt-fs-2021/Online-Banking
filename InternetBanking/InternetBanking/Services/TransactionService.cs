using InternetBanking.Data;
using InternetBanking.Exceptions;
using InternetBanking.Interfaces;
using InternetBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly McbaContext _context;

        public TransactionService(McbaContext context)
        {
            _context = context;
        }

        public async Task AddDepositTransactionAsync(int id, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account is null)
            {
                throw new NullReferenceException($"Unable to find account with {nameof(id)}");
            }
            account.Balance += amount;
            AddTransaction(account, TransactionType.Deposit, amount);

            await _context.SaveChangesAsync();
        }

        public async Task AddWithdrawTransactionAsync(int id, decimal amount)
        {

            var account = await _context.Accounts.FindAsync(id);

            if (account is null)
            {
                throw new NullReferenceException($"Unable to find account with {nameof(id)}");
            }

            if (!IsValidAmount(account, amount))
            {
                throw new AccountBalanceUpdateException($"Unable to update account by {nameof(amount)}");
            }

            account.Balance -= amount;
            AddTransaction(account, TransactionType.Withdraw, amount);

            await _context.SaveChangesAsync();
        }


        private void AddTransaction(Account account, TransactionType type, decimal amt)
        {
            account.Transactions.Add(
                new Transaction
                {
                    TransactionType = type,
                    Amount = amt,
                    TransactionTimeUtc = DateTime.UtcNow
                });
        }

        private bool IsValidAmount(Account account, decimal amount)
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
