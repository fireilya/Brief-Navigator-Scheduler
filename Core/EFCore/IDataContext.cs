using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.EFCore;

public interface IDataContext : IDisposable
{
    Task InsertAsync<TEntity>(TEntity entity) where TEntity : class;
    Task InsertRangeAsync<TEntity>(params TEntity[] entities) where TEntity : class;
    Task<TEntity?> FindAsync<TEntity, TKey>(TKey primaryKey) where TEntity : class;
    Task<TEntity> ReadAsync<TEntity, TKey>(TKey primaryKey) where TEntity : class;

    Task<TEntity[]> SelectAsync<TEntity, TKey>(
        Expression<Func<TEntity, TKey>> propertyPicker,
        params TKey[] values
    ) where TEntity : class;

    Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;

    Task UpdatePropertiesAsync<TEntity, TKey>(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperties,
        Expression<Func<TEntity, TKey>> primaryKeyPicker,
        params TKey[] keys
    ) where TEntity : class;

    Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class;

    Task DeleteAsync<TEntity, TKey>(Expression<Func<TEntity, TKey>> primaryKeyPicker, params TKey[] keys)
        where TEntity : class;

    IQueryable<TEntity> GetTable<TEntity>() where TEntity : class;
}