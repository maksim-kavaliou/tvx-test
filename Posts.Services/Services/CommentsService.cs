using Posts.DataAccess.Interfaces;
using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces;
using Posts.Services.Services.Base;

namespace Posts.Services.Services
{
    public class CommentsService :  EntityService<Comment>, ICommentsService
    {
        public CommentsService(ICommentsRepository entityRepository) : base(entityRepository)
        {
        }
    }
}
