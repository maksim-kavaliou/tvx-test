using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Posts.DataAccess.Interfaces.Base;
using Posts.DomainEntities.Entities.Base;

namespace Posts.DataAccess.Repositories.Base
{
    public abstract class EntityRepository<T> : IEntityRepository<T>
        where T : DomainEntity
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> EntitySet;

        protected EntityRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            EntitySet = DbContext.Set<T>();
        }

        public T Get(int id)
        {
            var result = EntitySet.FirstOrDefault(e => e.Id == id);

            return result;
        }

        public IList<T> GetList()
        {
            var result = EntitySet.ToList();

            return result;
        }

        public T Create(T entity)
        {
            EntitySet.Add(entity);
            DbContext.SaveChanges();

            return entity;
        }

        public void Update(T entity)
        {
            EntitySet.Attach(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = EntitySet.Find(id);
            CleanDependencies();
            EntitySet.Remove(entity);
            DbContext.SaveChanges();
        }

        public virtual void CleanDependencies()
        {
            // override if it's needed
        }
    }
}
