using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdminApi.Models.Repository
{
    /// <summary>
    /// Code referenced from Matthew Bolger's Tut/Lab 09.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IDataRepository<TEntity, TKey> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(TKey id);

    }
}
