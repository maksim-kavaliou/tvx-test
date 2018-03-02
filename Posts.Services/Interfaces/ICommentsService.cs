using System.Collections.Generic;
using System.Threading.Tasks;
using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces.Base;

namespace Posts.Services.Interfaces
{
    public interface ICommentsService : IEntityService<Comment>
    {
        Task<IList<Comment>> GetByPostId(int postId);
    }
}
