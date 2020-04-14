using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFrameworkCore.L.Extension.Extension
{
    public interface IEntityFrameworkCoreExtension<TDbContext,TEntity>: IQueryable<TEntity> where TDbContext : class where TEntity : class
    {
        Type ElementType { get; }
        Expression Expression { get; }
        IQueryProvider Provider { get; }

        TEntity Add(TEntity entity);
        System.Collections.Generic.IEnumerable<TEntity> AddRange(System.Collections.Generic.IEnumerable<TEntity> entities);
        TEntity Delete(TEntity entity);
        System.Collections.Generic.IEnumerable<TEntity> DeleteRange(System.Collections.Generic.IEnumerable<TEntity> entities);
        System.Collections.Generic.IEnumerator<TEntity> GetEnumerator();
        System.Collections.Generic.IEnumerable<TEntity> GetResource();
        Task<System.Collections.Generic.IEnumerable<TEntity>> ToListAsync();
        Task<System.Collections.Generic.IEnumerable<TEntity>> GetResourceAsync<TProperty>(System.Collections.Generic.List<Expression<Func<TEntity, TProperty>>> expressions);
        Task<System.Collections.Generic.IEnumerable<TEntity>> getResourceListByPage<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderBy, BlogParameters blogParameters);
        IEntityFrameworkCoreExtension<TDbContext, TEntity> GetResources();
        IEntityFrameworkCoreExtension<TDbContext, TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> include);
        TEntity Modify(TEntity entity);
        System.Collections.Generic.IEnumerable<TEntity> ModifyRange(System.Collections.Generic.IEnumerable<TEntity> entities);
        IEntityFrameworkCoreExtension<TDbContext, TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> orderBy);
        IEntityFrameworkCoreExtension<TDbContext, TEntity> ThenInclude<TPreviousProperty, TProperty>(Expression<Func<TEntity, System.Collections.Generic.IEnumerable<TPreviousProperty>>> include, Expression<Func<TPreviousProperty, TProperty>> thenInclude);  
        IEntityFrameworkCoreExtension<TDbContext, TEntity> Where(Expression<Func<TEntity, bool>> where);
    }
}