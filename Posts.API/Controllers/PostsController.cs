using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Posts.API.Controllers.Base;
using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces;
using Posts.Web.Core.Models;

namespace Posts.API.Controllers
{
    [Route("api/posts")]
    public class PostsController : BaseEntityController<Post, PostModel>
    {
        public PostsController(IMapper mapper, IPostsService service) : base(mapper, service)
        {
        }

        [HttpGet]
        public ActionResult Get()
        {
            return GetAction();
        }

        [HttpGet("{id}")]
        public virtual ActionResult Get(int id)
        {
            return GetAction(id);
        }

        [HttpPost]
        public virtual ActionResult Post([FromBody]PostModel model)
        {
            return PostAction(model);
        }

        [HttpPut("{id}")]
        public virtual ActionResult Put(int id, [FromBody]PostModel model)
        {
            return PutAction(id, model);
        }

        [HttpDelete("{id}")]
        public virtual ActionResult Delete(int id)
        {
            return DeleteAction(id);
        }
    }
}
