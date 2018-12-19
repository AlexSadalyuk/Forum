using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstExample.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        int Count { get; }

        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        TEntity GetFullById(int id);
        IQueryable<TEntity> GetFull();

        IEnumerable<TEntity> GetFullRange<TKey>(int skip, int take,
            Expression<Func<TEntity, TKey>> expression);

        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);

    }
}
