using InternetBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Interfaces
{
    public interface ITransactionService
    {
        Task AddBillPayTransaction(Account account, decimal amount);
        Task AddDepositTransactionAsync(Account account, decimal amount);
        Task AddTransferTransactionAsync(Account srcAccount, Account destAccount, decimal amount, string comment = null);
        Task AddWithdrawTransactionAsync(Account account, decimal amount);
    }
}
