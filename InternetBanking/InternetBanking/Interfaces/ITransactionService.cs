using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Interfaces
{
    public interface ITransactionService
    {
        Task AddDepositTransactionAsync(int id, decimal amount);
        Task AddWithdrawTransactionAsync(int id, decimal amount);
    }
}
