using EntityFrameworkCore.L.Extension.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFrameworkCore.L.Extension.Extension
{
    public interface IEntityFrameworkCoreExtension<TDbContext, TEntity> : IQueryable<TEntity> where TDbContext : DbContext where TEntity : class
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        TEntity Delete(TEntity entity);
        TEntity Delete(Expression<Func<TEntity, bool>> query);
        IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entities);
        IEntityFrameworkCoreExtension<TDbContext, TEntity> GetEntities();
        TEntity GetEntity<TKey>(TKey key);
        Task<TEntity> GetEntityAsync<TKey>(TKey key);
        Task<IEnumerable<TEntity>> GetEntitiesAsync();
        Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> query);
        Task<IEnumerable<TEntity>> GetEntitiesAsync<TProperty>(List<Expression<Func<TEntity, TProperty>>> expressions);
        Task<IEnumerable<TResult>> GetEntitiesAsync<TResult, TParameter, TKey>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TResult>> selectEntity, TParameter queryParameter) where TParameter : QueryParameter<TEntity, TKey>;
        Task<IEnumerable<TEntity>> GetEntitiesAsync<TParameter,TKey>(Expression<Func<TEntity, bool>> query,  TParameter queryParameter) where TParameter : QueryParameter<TEntity, TKey>; 
        Task<IEnumerable<TEntity>> GetEntitiesAsync<TKey>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TKey>> orderBy, int pageIndex, int pageSize);
        IEntityFrameworkCoreExtension<TDbContext, TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> include);
        IEntityFrameworkCoreExtension<TDbContext, TEntity> Include<TPreviousProperty, TProperty>(Expression<Func<TEntity, System.Collections.Generic.IEnumerable<TPreviousProperty>>> include, Expression<Func<TPreviousProperty, TProperty>> thenInclude);
        TEntity Modify(TEntity entity);
        IEnumerable<TEntity> ModifyRange(IEnumerable<TEntity> entities);
        IEntityFrameworkCoreExtension<TDbContext, TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> orderBy);
        Task<IEnumerable<TEntity>> ToListAsync();
        IEntityFrameworkCoreExtension<TDbContext, TEntity> Where(Expression<Func<TEntity, bool>> query);
       
    }
}