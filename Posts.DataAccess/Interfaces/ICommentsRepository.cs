using System.Collections.Generic;
using Posts.DataAccess.Interfaces.Base;
using Posts.DomainEntities.Entities;

namespace Posts.DataAccess.Interfaces
{
    public interface ICommentsRepository : IEntityRepository<Comment>
    {
        IList<Comment> GetByPostId(int postId);
    }
}
