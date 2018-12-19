using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstExample.Core.Entities.Domain;

namespace FirstExample.BusinessLogic.Helpers
{
    public static class TokenGenerator
    {
        public static Token GenerateToken()
        {
            DateTime expireDate = DateTime.Now;
            TimeSpan time = new TimeSpan(2, 0, 0, 0);
            expireDate = expireDate.Add(time);

            Token token = new Token
            {
                ConfirmationToken = Guid.NewGuid().ToString(),
                ExpirationDate = expireDate
            };

            return token;
        }
    }
}
