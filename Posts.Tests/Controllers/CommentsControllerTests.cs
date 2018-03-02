using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Posts.Tests.Common;
using Posts.Web.Core.Models;
using Xunit;

namespace Posts.Tests.Controllers
{
    public class CommentsControllerTests
    {
        private readonly HttpClient _client;

        public CommentsControllerTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task Routing_To_CommentsGet_Test()
        {
            var response = await _client.GetAsync("/api/posts/5/comments");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentArray = JsonConvert.DeserializeObject<List<CommentModel>>(content);

            Assert.NotNull(contentArray);
            Assert.True(contentArray.Count > 0);
        }

        [Fact]
        public async Task Routing_To_CommentsGetById_Test()
        {
            var response = await _client.GetAsync("/api/posts/5/comments/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JsonConvert.DeserializeObject<CommentModel>(content);

            Assert.NotNull(contentObject);
            Assert.NotNull(contentObject.Author);
            Assert.NotNull(contentObject.Content);
        }

        [Theory]
        [InlineData("empty", HttpStatusCode.BadRequest)]
        [InlineData("random", HttpStatusCode.OK)]
        public async Task Routing_To_CommentsPost_ValidateModel_Test(string modelType, HttpStatusCode statusCode)
        {
            var response = await _client.PostAsync("/api/posts/5/comments", ContentHelper.AsJson(EntityBuilder.CommentModelResolver[modelType]()));

            Assert.Equal(statusCode, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JObject.Parse(content);

            Assert.NotNull(contentObject);
        }

        [Theory]
        [InlineData("empty", HttpStatusCode.BadRequest, 3)]
        [InlineData("random", HttpStatusCode.OK, 4)]
        public async Task Routing_To_CommentsPut_ValidateModel_Test(string modelType, HttpStatusCode statusCode, int i)
        {
            var response = await _client.PutAsync($"/api/posts/5/comments/{i}", ContentHelper.AsJson(EntityBuilder.CommentModelResolver[modelType]()));

            Assert.Equal(statusCode, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JObject.Parse(content);

            Assert.NotNull(contentObject);
        }

        [Fact]
        public async Task Routing_To_CommentsDelete_Test()
        {
            var response = await _client.DeleteAsync("/api/posts/5/comments/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JObject.Parse(content);

            Assert.NotNull(contentObject);
        }
    }
}
