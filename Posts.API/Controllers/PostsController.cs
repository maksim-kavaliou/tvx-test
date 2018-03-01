using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Posts.API.Controllers.Base;
using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces;
using Posts.Web.Core.Models;

namespace Posts.API.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : BaseEntityController<Post, PostModel>
    {
        public PostsController(IMapper mapper, IPostsService service) : base(mapper, service)
        {
        }
    }
}
