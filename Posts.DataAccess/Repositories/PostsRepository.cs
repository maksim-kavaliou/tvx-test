using Microsoft.EntityFrameworkCore;
using Posts.DataAccess.Interfaces;
using Posts.DataAccess.Repositories.Base;
using Posts.DomainEntities.Entities;

namespace Posts.DataAccess.Repositories
{
    public class PostsRepository : EntityRepository<Post>, IPostsRepository
    {
        public PostsRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
