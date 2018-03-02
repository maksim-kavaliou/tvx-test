using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IList<Comment>> GetByPostId(int postId)
        {
            var result = await DbContext.Comments.Where(c => c.PostId == postId).ToListAsync();

            return result;
        }
    }
}
