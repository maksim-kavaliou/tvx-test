using System.Threading.Tasks;
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
        public async Task<ActionResult> Get()
        {
            return await GetActionAsync();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult> Get(int id)
        {
            return await GetActionAsync(id);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([FromBody]PostModel model)
        {
            return await PostActionAsync(model);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Put(int id, [FromBody]PostModel model)
        {
            return await PutActionAsync(id, model);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            return await DeleteActionAsync(id);
        }
    }
}
