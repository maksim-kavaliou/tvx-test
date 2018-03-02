using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Posts.DataAccess.Context;
using Posts.DataAccess.Interfaces.Base;
using Posts.DomainEntities.Entities.Base;

namespace Posts.DataAccess.Repositories.Base
{
    public abstract class EntityRepository<T> : IEntityRepository<T>
        where T : DomainEntity
    {
        protected readonly DefaultDbContext DbContext;
        protected readonly DbSet<T> EntitySet;

        protected EntityRepository(DefaultDbContext dbContext)
        {
            DbContext = dbContext;
            EntitySet = DbContext.Set<T>();
        }

        public async Task<T> Get(int id)
        {
            var result = await EntitySet.FirstOrDefaultAsync(e => e.Id == id);

            return result;
        }

        public async Task<IList<T>> GetList()
        {
            var result = await EntitySet.ToListAsync();

            return result;
        }

        public async Task<T> Create(T entity)
        {
            EntitySet.Add(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Update(T entity)
        {
            var original = await EntitySet.FindAsync(entity.Id);

            // field can't be changed
            entity.CreatedOn = original.CreatedOn;

            DbContext.Entry(original).CurrentValues.SetValues(entity);
            DbContext.Entry(original).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = EntitySet.Find(id);
            EntitySet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
