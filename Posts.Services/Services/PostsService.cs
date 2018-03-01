using Posts.DataAccess.Interfaces;
using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces;
using Posts.Services.Services.Base;

namespace Posts.Services.Services
{
    public class PostsService : EntityService<Post>, IPostsService
    {
        public PostsService(IPostsRepository entityRepository) : base(entityRepository)
        {
        }
    }
}
