using System.Collections.Generic;
using Posts.DataAccess.Interfaces.Base;
using Posts.DomainEntities.Entities.Base;
using Posts.Services.Interfaces.Base;

namespace Posts.Services.Services.Base
{
    public abstract class EntityService<T> : IEntityService<T>
        where T : DomainEntity
    {
        protected readonly IEntityRepository<T> Repository;

        protected EntityService(IEntityRepository<T> entityRepository)
        {
            Repository = entityRepository;
        }

        public T Get(int id)
        {
            return Repository.Get(id);
        }

        public IList<T> GetList()
        {
            return Repository.GetList();
        }

        public T Create(T entity)
        {
            return Repository.Create(entity);
        }

        public void Update(T entity)
        {
            Repository.Update(entity);
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
        }
    }
}
