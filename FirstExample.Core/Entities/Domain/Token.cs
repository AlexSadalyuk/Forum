using System;

namespace FirstExample.Core.Entities.Domain
{
    public class Token
    {
        public int Id { get; set; }
        public string ConfirmationToken { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
