using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Posts.API;
using Posts.DomainEntities.Entities;
using Posts.Services.Interfaces;
using Posts.Web.Core.Mappers;

namespace Posts.Tests.Common
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile)))));

            services.Add(ServiceDescriptor.Singleton<IPostsService>(ConfigurePostsServiceMock().Object));
            services.Add(ServiceDescriptor.Singleton<ICommentsService>(ConfigureCommentsServiceMock().Object));
        }

        private Mock<IPostsService> ConfigurePostsServiceMock()
        {
            var postsServiceMock = new Mock<IPostsService>();

            postsServiceMock.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(EntityBuilder.CreatePostEntity());
            postsServiceMock.Setup(s => s.GetList()).ReturnsAsync(new List<Post>()
            {
                EntityBuilder.CreatePostEntity(),
                EntityBuilder.CreatePostEntity()
            });

            return postsServiceMock;
        }

        private Mock<ICommentsService> ConfigureCommentsServiceMock()
        {
            var commentsServiceMock = new Mock<ICommentsService>();

            commentsServiceMock.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(EntityBuilder.CreateCommentEntity());
            commentsServiceMock.Setup(s => s.GetByPostId(It.IsAny<int>())).ReturnsAsync(new List<Comment>()
            {
                EntityBuilder.CreateCommentEntity(),
                EntityBuilder.CreateCommentEntity()
            });

            return commentsServiceMock;
        }
    }
}
