using System.Collections.Generic;
using System.Threading.Tasks;
using Posts.DomainEntities.Entities.Base;

namespace Posts.DataAccess.Interfaces.Base
{
    public interface IEntityRepository<T> where T : DomainEntity
    {
        Task<T> Get(int id);

        Task<IList<T>> GetList();

        Task<T> Create(T entity);

        Task Update(T entity);

        Task Delete(int id);
    }
}
