using System.Collections.Generic;
using System.Threading.Tasks;
using Posts.DataAccess.Interfaces;
using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces;
using Posts.Services.Services.Base;

namespace Posts.Services.Services
{
    public class CommentsService :  EntityService<Comment>, ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsService(ICommentsRepository entityRepository) : base(entityRepository)
        {
            _commentsRepository = entityRepository;
        }

        public async Task<IList<Comment>> GetByPostId(int postId)
        {
            return await _commentsRepository.GetByPostId(postId);
        }
    }
}
