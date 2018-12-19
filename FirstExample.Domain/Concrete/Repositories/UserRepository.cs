using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using FirstExample.Core.Entities.Domain;
using FirstExample.Core.Interfaces.Repositories;

namespace FirstExample.Domain.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DBContext context)
            : base(context)
        {

        }

        //Get deep versions of users
        public override IQueryable<User> GetFull()
        {
            return _context.Users.Include(u => u.Posts)
                                 .Include(u => u.Comments);
        }

        //Get single deep version 
        public override User GetFullById(int id)
        {
            return GetFull().FirstOrDefault(u => u.Id == id);
        }

        //Get deep version by name
        public User GetByName(string name)
        {
            return GetFull().FirstOrDefault(u => u.Name == name);
        }

        //Cascade removing foreign keys
        public override User Remove(User entity)
        {
            User user = _context.Users.Include(x => x.Posts)
                                      .Include(x => x.Comments)
                                      .FirstOrDefault(x => x.Name == entity.Name);
            return base.Remove(entity);
        }
    }
}
