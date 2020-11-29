using CoffeShop.DataProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CoffeeShopContext Context;
        protected readonly DbSet<TEntity> DbSetEntity;


        public Repository(CoffeeShopContext context)
        {
            Context = context;
            DbSetEntity = Context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            DbSetEntity.Add(entity);
        }
        public void AddOrUpdate(TEntity entity)
        {
            DbSetEntity.AddOrUpdate(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSetEntity.AddRange(entities);
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSetEntity.Where(predicate);
        }
        public TEntity FindById(Guid id)
        {
            return DbSetEntity.Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
           return DbSetEntity.ToList();
        }
        public void Remove(TEntity entity)
        {
            DbSetEntity.Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSetEntity.RemoveRange(entities);
        }
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSetEntity.SingleOrDefault(predicate);
        }
    }
}
