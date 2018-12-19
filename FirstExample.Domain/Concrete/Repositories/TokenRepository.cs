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
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(DBContext context)
            : base(context)
        {

        }

        public Token GetByToken(string token)
        {
            return GetFull().FirstOrDefault(x => x.ConfirmationToken == token);
        }

        public override IQueryable<Token> GetFull()
        {
            return _context.Tokens.Include(x=>x.User);
        }

        public override Token GetFullById(int id)
        {
            return GetFull().FirstOrDefault(x => x.Id == id);
        }
    }
}
