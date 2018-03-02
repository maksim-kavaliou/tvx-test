using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces.Base;

namespace Posts.Services.Interfaces
{
    public interface ICommentsService : IEntityService<Comment>
    {
    }
}
