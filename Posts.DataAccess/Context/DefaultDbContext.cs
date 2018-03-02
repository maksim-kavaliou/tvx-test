using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Posts.DomainEntities.Entities;
using Posts.DomainEntities.Entities.Base;

namespace Posts.DataAccess.Context
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext()
        {
        }

        public DefaultDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }

        private void Audit()
        {
            var entries = ChangeTracker
                    .Entries()
                    .Where(x => x.Entity is DomainEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((DomainEntity)entry.Entity).CreatedOn = DateTime.UtcNow;
                }
                ((DomainEntity)entry.Entity).ModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
