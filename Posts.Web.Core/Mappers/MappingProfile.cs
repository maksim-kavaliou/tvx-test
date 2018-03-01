using AutoMapper;
using Posts.DomainEntities.Entities;
using Posts.Web.Core.Models;

namespace Posts.Web.Core.Mappers
{
    public class MappingProfile : Profile
    {
        public  MappingProfile()
        {
            CreateMap<Post, PostModel>();
            CreateMap<PostModel, Post>();

            CreateMap<Comment, CommentModel>();
            CreateMap<CommentModel, Comment>();
        }
    }
}
