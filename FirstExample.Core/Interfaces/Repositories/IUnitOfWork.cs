using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstExample.Core.Entities.Domain;

namespace FirstExample.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICommentRepository Comments { get; }
        IPostRepository Posts { get; }
        ITokenRepository Tokens { get; }
        int Complete();
    }
}
