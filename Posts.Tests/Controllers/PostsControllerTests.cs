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
    public class PostsControllerTests
    {
        private readonly HttpClient _client;

        public PostsControllerTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task Routing_To_PostsGet_Test()
        {
            var response = await _client.GetAsync("/api/posts");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentArray = JsonConvert.DeserializeObject<List<PostModel>>(content);

            Assert.NotNull(contentArray);
            Assert.True(contentArray.Count > 0);
        }

        [Fact]
        public async Task Routing_To_PostsGetById_Test()
        {
            var response = await _client.GetAsync("/api/posts/5");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JsonConvert.DeserializeObject<PostModel>(content);

            Assert.NotNull(contentObject);
            Assert.NotNull(contentObject.Title);
            Assert.NotNull(contentObject.Author);
            Assert.NotNull(contentObject.Content);
        }

        [Theory]
        [InlineData("empty", HttpStatusCode.BadRequest)]
        [InlineData("random", HttpStatusCode.OK)]
        public async Task Routing_To_PostsPost_ValidateModel_Test(string modelType, HttpStatusCode statusCode)
        {
            var response = await _client.PostAsync("/api/posts", ContentHelper.AsJson(EntityBuilder.PostModelResolver[modelType]()));

            Assert.Equal(statusCode, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JObject.Parse(content);

            Assert.NotNull(contentObject);
        }

        [Theory]
        [InlineData("empty", HttpStatusCode.BadRequest)]
        [InlineData("random", HttpStatusCode.OK)]
        public async Task Routing_To_PostsPut_ValidateModel_Test(string modelType, HttpStatusCode statusCode)
        {
            var response = await _client.PutAsync("/api/posts/5", ContentHelper.AsJson(EntityBuilder.PostModelResolver[modelType]()));

            Assert.Equal(statusCode, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JObject.Parse(content);

            Assert.NotNull(contentObject);
        }

        [Fact]
        public async Task Routing_To_PostsDelete_Test()
        {
            var response = await _client.DeleteAsync("/api/posts/5");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JObject.Parse(content);

            Assert.NotNull(contentObject);
        }
    }
}
