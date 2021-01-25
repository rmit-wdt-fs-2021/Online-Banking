using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Models.Repository
{
    public interface ITransactionRepository : IDataRepository<Transaction, int>
    {
        IEnumerable<Transaction> GetTransactions(int accountNumber, DateTime? fromDate = null, DateTime? toDate = null);
    }
}
