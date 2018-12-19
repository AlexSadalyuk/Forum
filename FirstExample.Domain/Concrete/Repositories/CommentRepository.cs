using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FirstExample.Core.Entities.Domain;
using FirstExample.Core.Interfaces.Repositories;

namespace FirstExample.Domain.Concrete
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DBContext context)
            : base(context)
        {

        }

        public override IQueryable<Comment> GetFull()
        {
            return _context.Comments.Include(c => c.Post)
                                    .Include(c => c.User);
        }

        public override Comment GetFullById(int id)
        {
            return GetFull().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Comment> GetFullRange<TKey>(int postId, int skip, int take,
            System.Linq.Expressions.Expression<Func<Comment, TKey>> expression)
        {
            return GetByPost(postId).OrderBy(expression)
                                    .Skip(skip)
                                    .Take(take)
                                    .AsEnumerable();
        }

        //Get deep versions by post
        public IQueryable<Comment> GetByPost(int postId)
        {
            return GetFull().Where(x=>x.PostId == postId);
        }
    }
}
