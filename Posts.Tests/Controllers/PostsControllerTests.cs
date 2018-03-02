using System.Net;
using System.Threading.Tasks;
using Posts.Tests.Common;
using Posts.Tests.Controllers.Base;
using Posts.Web.Core.Models;
using Xunit;

namespace Posts.Tests.Controllers
{
    public class PostsControllerTests : BaseControllerTests
    {
        [Fact]
        public async Task Routing_To_PostsGet_Test()
        {
            await RoutingToGetTest<PostModel>("/api/posts");
        }

        [Fact]
        public async Task Routing_To_PostsGetById_Test()
        {
            await RoutingToGetByIdTest<PostModel>("/api/posts/5",
                (model) =>
                {
                    Assert.NotNull(model.Title);
                    Assert.NotNull(model.Author);
                    Assert.NotNull(model.Content);
                });
        }

        [Theory]
        [InlineData("empty", HttpStatusCode.BadRequest)]
        [InlineData("random", HttpStatusCode.OK)]
        public async Task Routing_To_PostsPost_ValidateModel_Test(string modelType, HttpStatusCode statusCode)
        {
            await RoutingToPostValidateModelTest<PostModel>("/api/posts", EntityBuilder.PostModelResolver[modelType](), statusCode);
        }

        [Theory]
        [InlineData("empty", HttpStatusCode.BadRequest, 3)]
        [InlineData("random", HttpStatusCode.OK, 4)]
        public async Task Routing_To_PostsPut_ValidateModel_Test(string modelType, HttpStatusCode statusCode, int i)
        {
            await RoutingToPutValidateModelTest<PostModel>($"/api/posts/{i}", EntityBuilder.PostModelResolver[modelType](), statusCode);
        }

        [Fact]
        public async Task Routing_To_PostsDelete_Test()
        {
            await RoutingToDeleteTest("/api/posts/5");
        }
    }
}
