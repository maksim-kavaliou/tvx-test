using Posts.DataAccess.Context;
using Posts.DataAccess.Interfaces;
using Posts.DataAccess.Repositories.Base;
using Posts.DomainEntities.Entities;

namespace Posts.DataAccess.Repositories
{
    public class CommentsRepository : EntityRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(DefaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}
