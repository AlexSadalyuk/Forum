using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstExample.Core.Interfaces.Repositories;

namespace FirstExample.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;

        public UnitOfWork(DBContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Comments = new CommentRepository(_context);
            Posts = new PostRepository(_context);
            Tokens = new TokenRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public ICommentRepository Comments { get; private set; }

        public IPostRepository Posts { get; private set; }

        public ITokenRepository Tokens { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
