using System;
using System.Collections.Generic;

namespace FirstExample.Core.Entities.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        public bool EmailConfirmed { get; set; }

        public int? TokenId { get; set; }
        public Token Token { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public User()
        {
            Posts = new List<Post>();
            Comments = new List<Comment>();
        }
    }
}
