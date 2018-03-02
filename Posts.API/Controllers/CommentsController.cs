using System.Collections.Generic;
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
        public ActionResult Get(int postId)
        {
            var entities = _commentsService.GetByPostId(postId);

            var models = Mapper.Map<IList<CommentModel>>(entities);

            return Json(models);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int postId, int id)
        {
            var entity = Service.Get(id);

            var model = Mapper.Map<CommentModel>(entity);

            return Json(model);
        }

        [HttpPost]
        public ActionResult Post(int postId, [FromBody]CommentModel model)
        {
            var entity = Mapper.Map<Comment>(model);
            entity.PostId = postId;

            Service.Create(entity);

            return Json(new { success = true });
        }

        [HttpPut("{id}")]
        public ActionResult Put(int postId, int id, [FromBody]CommentModel model)
        {
            var entity = Mapper.Map<Comment>(model);
            entity.Id = id;
            entity.PostId = postId;

            Service.Update(entity);

            return Json(new { success = true });
        }

        [HttpDelete("{id}")]
        public virtual ActionResult Delete(int postId, int id)
        {
            Service.Delete(id);

            return Json(new { success = true });
        }
    }
}
