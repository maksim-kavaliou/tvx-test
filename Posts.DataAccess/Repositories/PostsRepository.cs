using Posts.DataAccess.Context;
using Posts.DataAccess.Interfaces;
using Posts.DataAccess.Repositories.Base;
using Posts.DomainEntities.Entities;

namespace Posts.DataAccess.Repositories
{
    public class PostsRepository : EntityRepository<Post>, IPostsRepository
    {
        public PostsRepository(DefaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}
