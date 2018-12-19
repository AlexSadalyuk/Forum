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
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DBContext context)
            : base(context)
        {
        }


        //Get by author
        public IEnumerable<Post> GetFullByUserName(string name)
        {
            return GetFull().Where(x => x.User.Name == name);
        }

        //Get deep version
        public override IQueryable<Post> GetFull()
        {
            return _context.Posts.Include(p => p.User)
                                 .Include(p => p.Comments.Select(c=>c.User));
        }

        //Get deep single verison
        public override Post GetFullById(int id)
        {
            return GetFull().FirstOrDefault(x => x.Id == id);
        }
    }
}
