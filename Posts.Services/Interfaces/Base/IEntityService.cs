using System.Collections.Generic;
using Posts.DomainEntities.Entities.Base;

namespace Posts.Services.Interfaces.Base
{
    public interface IEntityService<T> 
        where T: DomainEntity
    {
        T Get(int id);

        IList<T> GetList();

        T Create(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
