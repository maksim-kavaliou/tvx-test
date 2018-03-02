using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<T> Get(int id)
        {
            return await Repository.Get(id);
        }

        public async Task<IList<T>> GetList()
        {
            return await Repository.GetList();
        }

        public async Task<T> Create(T entity)
        {
            return await Repository.Create(entity);
        }

        public async Task Update(T entity)
        {
            await Repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await Repository.Delete(id);
        }
    }
}
