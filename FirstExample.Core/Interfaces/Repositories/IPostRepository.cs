using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstExample.Core.Entities.Domain;


namespace FirstExample.Core.Interfaces.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetFullByUserName(string name);
    }
}
