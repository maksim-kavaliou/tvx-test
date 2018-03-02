using System.Collections.Generic;
using System.Threading.Tasks;
using Posts.DataAccess.Interfaces.Base;
using Posts.DomainEntities.Entities;

namespace Posts.DataAccess.Interfaces
{
    public interface ICommentsRepository : IEntityRepository<Comment>
    {
        Task<IList<Comment>> GetByPostId(int postId);
    }
}
