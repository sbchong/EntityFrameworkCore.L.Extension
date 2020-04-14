using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.L.Extension.Extension
{
    public class EntityFrameworkCoreExtension : IEntityFrameworkCoreExtension
    {
        private readonly TDbContext _db;
        private IQueryable<TEntity> data;

        public Type ElementType => throw new NotImplementedException();

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();

        public EFCoreHelper(TDbContext db)
        {
            _db = db;
        }
        public IEFCoreHelper<TDbContext, TEntity> GetResources()
        {
            data = _db.Set<TEntity>();
            return this;
        }

        public IEFCoreHelper<TDbContext, TEntity> Where(Expression<Func<TEntity, bool>> where)
        {
            data = data.Where(where);
            return this;
        }
        public IEFCoreHelper<TDbContext, TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> orderBy)
        {
            data = data.OrderBy(orderBy);
            return this;
        }
        public IEFCoreHelper<TDbContext, TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            data = data.Include(include);
            return this;
        }
        public IEFCoreHelper<TDbContext, TEntity> ThenInclude<TPreviousProperty, TProperty>(Expression<Func<TEntity, IEnumerable<TPreviousProperty>>> include, Expression<Func<TPreviousProperty, TProperty>> thenInclude)
        {
            data = data.Include(include).ThenInclude(thenInclude);

            return this;
        }

        public async Task<IEnumerable<TEntity>> getResourceListByPage<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderBy, BlogParameters blogParameters)
        {
            return await _db.Set<TEntity>().Where(where).OrderByDescending(orderBy).Skip(blogParameters.PageIndex * blogParameters.PageSize).Take(blogParameters.PageSize).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetResourceAsync<TProperty>(List<Expression<Func<TEntity, TProperty>>> expressions)
        {
            var entity = _db.Set<TEntity>() as IQueryable<TEntity>;

            foreach (var include in expressions)
            {
                entity = entity.Include(include);
            }

            return await entity.ToListAsync();
        }

        public IEnumerable<TEntity> GetResource()
        {
            return _db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> ToListAsync()
        {
            return await data.ToListAsync();
        }

        public TEntity Add(TEntity entity)
        {
            _db.Entry<TEntity>(entity).State = EntityState.Added;
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _db.Entry<TEntity>(entity).State = EntityState.Added;
            }
            return entities;
        }

        public TEntity Modify(TEntity entity)
        {
            _db.Entry<TEntity>(entity).State = EntityState.Modified;
            return entity;
        }
        public IEnumerable<TEntity> ModifyRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _db.Entry<TEntity>(entity).State = EntityState.Modified;
            }
            return entities;
        }
        public TEntity Delete(TEntity entity)
        {
            _db.Entry<TEntity>(entity).State = EntityState.Deleted;
            return entity;
        }
        public IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _db.Entry<TEntity>(entity).State = EntityState.Deleted;
            }
            return entities;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
