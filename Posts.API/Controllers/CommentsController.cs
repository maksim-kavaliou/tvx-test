using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Posts.API.Controllers.Base;
using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces;
using Posts.Web.Core.Models;

namespace Posts.API.Controllers
{
    [Route("api/posts/{postid}/comments")]
    public class CommentsController : BaseEntityController<Comment, CommentModel>
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(IMapper mapper, ICommentsService service) : base(mapper, service)
        {
            _commentsService = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int postId)
        {
            var entities = await _commentsService.GetByPostId(postId);

            var models = Mapper.Map<IList<CommentModel>>(entities);

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int postId, int id)
        {
            var entity = await Service.Get(id);

            var model = Mapper.Map<CommentModel>(entity);

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int postId, [FromBody]CommentModel model)
        {
            var entity = Mapper.Map<Comment>(model);
            entity.PostId = postId;

            await Service.Create(entity);

            return Ok(new { success = true });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int postId, int id, [FromBody]CommentModel model)
        {
            var entity = Mapper.Map<Comment>(model);
            entity.Id = id;
            entity.PostId = postId;

            await Service.Update(entity);

            return Ok(new { success = true });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int postId, int id)
        {
            await Service.Delete(id);

            return Ok(new { success = true });
        }
    }
}
