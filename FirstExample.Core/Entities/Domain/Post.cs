using System;
using System.Collections.Generic;

namespace FirstExample.Core.Entities.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}
