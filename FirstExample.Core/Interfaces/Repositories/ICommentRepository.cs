using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FirstExample.Core.Entities.Domain;

namespace FirstExample.Core.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IQueryable<Comment> GetByPost(int postId);
        IEnumerable<Comment> GetFullRange<TKey>(int postId, int skip, int take,
            Expression<Func<Comment, TKey>> expression);
    }
}
