using System.Net;
using System.Threading.Tasks;
using Posts.Tests.Common;
using Posts.Tests.Controllers.Base;
using Posts.Web.Core.Models;
using Xunit;

namespace Posts.Tests.Controllers
{
    public class CommentsControllerTests : BaseControllerTests
    {
        [Fact]
        public async Task Routing_To_CommentsGet_Test()
        {
            await RoutingToGetTest<CommentModel>("/api/posts/5/comments");
        }

        [Fact]
        public async Task Routing_To_CommentsGetById_Test()
        {
            await RoutingToGetByIdTest<CommentModel>("/api/posts/5/comments/2",
            (model) =>
            {
                Assert.NotNull(model.Author);
                Assert.NotNull(model.Content);
            });
        }

        [Theory]
        [InlineData("empty", HttpStatusCode.BadRequest)]
        [InlineData("random", HttpStatusCode.OK)]
        public async Task Routing_To_CommentsPost_ValidateModel_Test(string modelType, HttpStatusCode statusCode)
        {
            await RoutingToPostValidateModelTest<CommentModel>("/api/posts/5/comments", EntityBuilder.CommentModelResolver[modelType](), statusCode);
        }

        [Theory]
        [InlineData("empty", HttpStatusCode.BadRequest, 3)]
        [InlineData("random", HttpStatusCode.OK, 4)]
        public async Task Routing_To_CommentsPut_ValidateModel_Test(string modelType, HttpStatusCode statusCode, int i)
        {
            await RoutingToPutValidateModelTest<CommentModel>($"/api/posts/5/comments/{i}", EntityBuilder.CommentModelResolver[modelType](), statusCode);
        }

        [Fact]
        public async Task Routing_To_CommentsDelete_Test()
        {
            await RoutingToDeleteTest("/api/posts/5/comments/2");
        }
    }
}
