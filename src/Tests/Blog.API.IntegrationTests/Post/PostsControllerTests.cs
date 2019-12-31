using Blog.API.Models.PostModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace Blog.API.IntegrationTests.Post
{
    //https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
    public class PostsControllerTests : IClassFixture<BlogApiFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PostsControllerTests(BlogApiFactory<Startup> factory)
        {
            //_client = factory.CreateClient();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Get_Posts_Paging()
        {
            // Arrange

            var queryString = ToQueryString(new PostPageOptionsModel
            {
                Title = "pos",
                Page = 1,
                Count = 2
            });
            var httpResponse = await _client.GetAsync($"/v1/Posts?{queryString}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var postPageResult = JsonConvert.DeserializeObject<PostPageResultModel>(stringResponse);

            //Assert

            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);

            Assert.True(postPageResult.TotalPages == 2);
            Assert.True(postPageResult.TotalCount == 3);
            Assert.True(postPageResult.Items.Count == 2);

            Assert.False(postPageResult.HasPreviousPage);
            Assert.True(postPageResult.HasNextPage);
        }

        #region Helpers

        private string ToQueryString(object o)
        {
            return string.Join
            (
                "&",
                ToPropertyDictionary(o)
                 .Where(e => e.Value != null)
                 .Select
                 (
                     e => string.Format
                     (
                         "{0}={1}",
                         e.Key,
                         HttpUtility.UrlEncode(e.Value.ToString())
                     )
                 )
            );
        }

        private Dictionary<string, object> ToPropertyDictionary(object o)
        {
            return o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreDataMemberAttribute)))
               .ToDictionary
               (
                   p => p.Name,
                   p => p.GetValue(o)
               );
        }

        #endregion
    }
}
