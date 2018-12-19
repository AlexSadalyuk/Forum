using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FirstExample.Core.Entities.Domain;

namespace FirstExample.Domain.Concrete
{
    public class DBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public DBContext()
            : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(user => user.Posts)
                        .WithOptional(post => post.User)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Token>()
                        .HasRequired(token => token.User)
                        .WithOptional(user => user.Token)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                        .HasMany(user => user.Comments)
                        .WithOptional(comment => comment.User)
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }

}

