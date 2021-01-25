using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdminApi.Models.Repository
{
    public interface IDataRepository<TEntity, TKey> where TEntity : class
    {
        // What do u want to do to database?
        
        //IEnumerable<Transaction> GetTransactions(int accountNumber, DateTime? fromDate = null, DateTime? toDate = null);

        TKey Update(TKey id, TEntity item);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(TKey id);

    }
}
