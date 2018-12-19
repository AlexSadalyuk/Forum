using System.Linq;
using System;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Collections.Generic;
using FirstExample.Core.Interfaces.Repositories;

namespace FirstExample.Domain.Concrete
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {

        public int Count
        {
            get
            {
                return _context.Set<TEntity>().Count();
            }
        }
        protected readonly DBContext _context;

        public Repository(DBContext context)
        {
            _context = context;
        }

        //Get by id
        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        //Get all items
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        //Add item
        public TEntity Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity);
        }

        //Remove item
        public virtual TEntity Remove(TEntity entity)
        {
            return _context.Set<TEntity>().Remove(entity);
        }

        //update item
        public virtual TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual IEnumerable<TEntity> GetFullRange<TKey>(int skip, int take,
            Expression<Func<TEntity,TKey>> expression)
        {
            return GetFull().OrderByDescending(expression)
                            .Skip(skip)
                            .Take(take)
                            .AsEnumerable();
        }

        //Get deep entity ver
        public abstract TEntity GetFullById(int id);
        public abstract IQueryable<TEntity> GetFull();
    }
}
