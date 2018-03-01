using Microsoft.EntityFrameworkCore;
using Posts.DomainEntities.Entities;

namespace Posts.DataAccess.Context
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
