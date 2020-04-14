using EntityFrameworkCore.L.Extension.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EntityFrameworkCore.L.Extension.Extension
{
    public partial class EntityFrameworkCoreExtension<TDbContext, TEntity> : IEntityFrameworkCoreExtension<TDbContext, TEntity> where TDbContext : DbContext where TEntity : class
    {
        private readonly TDbContext _db;
        private IQueryable<TEntity> data;

        public Type ElementType => throw new NotImplementedException();

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();

        public EntityFrameworkCoreExtension(TDbContext db)
        {
            _db = db;
        }


        public IEntityFrameworkCoreExtension<TDbContext, TEntity> Where(Expression<Func<TEntity, bool>> query)
        {
            data = data.Where(query);
            return this;
        }
        public IEntityFrameworkCoreExtension<TDbContext, TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> orderBy)
        {
            data = data.OrderBy(orderBy);
            return this;
        }


        public TEntity GetEntity<TKey>(TKey key)
        {
            return _db.Set<TEntity>().Find(key);
        }

        public async Task<TEntity> GetEntityAsync<TKey>(TKey key)
        {
            return await _db.Set<TEntity>().FindAsync(key);
        }
        public IEntityFrameworkCoreExtension<TDbContext, TEntity> GetEntities()
        {
            data = _db.Set<TEntity>();
            return this;
        }
        public async Task<IEnumerable<TEntity>> GetEntitiesAsync()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> query)
        {
            return await _db.Set<TEntity>().Where(query).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetEntitiesAsync<TParameter, TKey>(Expression<Func<TEntity, bool>> query, TParameter queryParameter) where TParameter : QueryParameter<TEntity, TKey>
        {
            return await _db.Set<TEntity>().Where(query).OrderByDescending(queryParameter.OrderBy).Skip(queryParameter.PageIndex * queryParameter.PageSize).Take(queryParameter.PageSize).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetEntitiesAsync<TKey>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TKey>> orderBy, int pageIndex, int pageSize)
        {
            return await _db.Set<TEntity>().Where(query).OrderByDescending(orderBy).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesAsync<TProperty>(List<Expression<Func<TEntity, TProperty>>> expressions)
        {
            var entity = _db.Set<TEntity>() as IQueryable<TEntity>;

            foreach (var include in expressions)
            {
                entity = entity.Include(include);
            }

            return await entity.ToListAsync();
        }
        public IEntityFrameworkCoreExtension<TDbContext, TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            data = data.Include(include);
            return this;
        }
        public IEntityFrameworkCoreExtension<TDbContext, TEntity> Include<TPreviousProperty, TProperty>(Expression<Func<TEntity, IEnumerable<TPreviousProperty>>> include, Expression<Func<TPreviousProperty, TProperty>> thenInclude)
        {
            data = data.Include(include).ThenInclude(thenInclude);

            return this;
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
        public TEntity Delete(Expression<Func<TEntity, bool>> query)
        {
            var entity = _db.Set<TEntity>().Find(query);
            if (!(entity is null))
            {
                _db.Entry<TEntity>(entity).State = EntityState.Deleted;
            }
            return entity;
        }

        public IEnumerator<TEntity> GetEnumerator() =>
            throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() =>
            throw new NotImplementedException();

    }
}
