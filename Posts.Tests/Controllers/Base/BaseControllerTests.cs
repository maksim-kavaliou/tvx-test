using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Posts.Tests.Common;
using Xunit;

namespace Posts.Tests.Controllers.Base
{
    public abstract class BaseControllerTests
    {
        protected readonly HttpClient Client;

        protected BaseControllerTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>());
            Client = server.CreateClient();
        }

        protected async Task RoutingToGetTest<T>(string uri)
        {
            var response = await Client.GetAsync(uri);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentArray = JsonConvert.DeserializeObject<List<T>>(content);

            Assert.NotNull(contentArray);
            Assert.True(contentArray.Count > 0);
        }

        protected async Task RoutingToGetByIdTest<T>(string uri, Action<T> assertModelAction = null)
        {
            var response = await Client.GetAsync(uri);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JsonConvert.DeserializeObject<T>(content);

            Assert.NotNull(contentObject);

            if (assertModelAction != null)
            {
                assertModelAction(contentObject);
            }
        }

        protected async Task RoutingToPostValidateModelTest<T>(string uri, T model, HttpStatusCode statusCode)
        {
            var response = await Client.PostAsync(uri, ContentHelper.AsJson(model));

            Assert.Equal(statusCode, response.StatusCode);
            await AssertJObjectContent(response.Content);
        }

        protected async Task RoutingToPutValidateModelTest<T>(string uri, T model, HttpStatusCode statusCode)
        {
            var response = await Client.PutAsync(uri, ContentHelper.AsJson(model));

            Assert.Equal(statusCode, response.StatusCode);
            await AssertJObjectContent(response.Content);
        }

        protected async Task RoutingToDeleteTest(string uri)
        {
            var response = await Client.DeleteAsync(uri);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            await AssertJObjectContent(response.Content);
        }

        private async Task AssertJObjectContent(HttpContent httpContent)
        {
            var content = await httpContent.ReadAsStringAsync();
            var contentObject = JObject.Parse(content);

            Assert.NotNull(contentObject);
        }
    }
}
