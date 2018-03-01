using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces.Base;

namespace Posts.Services.Interfaces
{
    public interface IPostsService : IEntityService<Post>
    {
    }
}
