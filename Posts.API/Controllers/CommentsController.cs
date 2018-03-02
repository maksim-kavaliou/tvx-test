using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Posts.API.Controllers.Base;
using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces;
using Posts.Web.Core.Models;

namespace Posts.API.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : BaseEntityController<Comment, CommentModel>
    {
        public CommentsController(IMapper mapper, ICommentsService service) : base(mapper, service)
        {
        }
    }
}
