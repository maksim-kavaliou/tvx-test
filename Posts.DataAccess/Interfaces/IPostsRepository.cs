using Posts.DataAccess.Interfaces.Base;
using Posts.DomainEntities.Entities;

namespace Posts.DataAccess.Interfaces
{
    public interface IPostsRepository : IEntityRepository<Post>
    {
    }
}
