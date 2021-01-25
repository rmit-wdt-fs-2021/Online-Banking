using AdminApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdminApi.Models.DataManager
{
    public class DataRepository<TEntity, TKey> : IDataRepository<TEntity, TKey> where TEntity : class
    {
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TKey Update(TKey id, TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
