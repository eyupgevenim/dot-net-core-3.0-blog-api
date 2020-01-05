using Blog.API.Models.PostModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace Blog.API.IntegrationTests.Controllers.V1
{
    //https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
    public class PostsControllerTests : IClassFixture<BlogApiFactory<Startup>>
    {
        protected string version { get; }
        protected readonly HttpClient _client;

        public PostsControllerTests(BlogApiFactory<Startup> factory, string version = "v1")
        {
            this.version = version;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [Fact]
        public virtual async Task Get_Posts_Paging()
        {
            // Arrange

            var queryString = ToQueryString(new PostPageOptionsModel
            {
                Title = "pos",
                Page = 1,
                Count = 2
            });
            var httpResponse = await _client.GetAsync($"/{version}/Posts?{queryString}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var postPageResult = JsonConvert.DeserializeObject<PostPageResultModel>(stringResponse);

            //Assert

            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);

            //Add_Post_Authorized_With_Token
            ///Assert.True(postPageResult.TotalPages == 2);
            ///Assert.True(postPageResult.TotalCount == 3);

            Assert.True(postPageResult.Items.Count == 2);

            Assert.False(postPageResult.HasPreviousPage);
            Assert.True(postPageResult.HasNextPage);
        }

        [Fact]
        public virtual async Task Add_Post_Unauthorized()
        {
            // Arrange

            var post = new PostModel
            {
                Title = "t post4",
                Body = "b post4",
                BodyOverview = "bo post4",
                Tags = "tg post4",
                MetaTitle = "mt post4",
                MetaKeywords = "mk post4",
                MetaDescription = "md post4",
                StartDateUtc = DateTime.UtcNow.AddHours(2),
                CreatedOnUtc = DateTime.UtcNow,
                EndDateUtc = DateTime.UtcNow.AddYears(5),
                AllowComments = true
            };
            var postContent = new StringContent(JsonConvert.SerializeObject(post), System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync($"/{version}/Posts", postContent);

            // Must be error.
            //httpResponse.EnsureSuccessStatusCode();

            //Assert

            Assert.Throws<HttpRequestException>(() => httpResponse.EnsureSuccessStatusCode());

            Assert.True(httpResponse.StatusCode == HttpStatusCode.Unauthorized);
        }

        [Fact]
        public virtual async Task Add_Post_Authorized_With_Token()
        {
            // Arrange

            var post = new PostModel
            {
                Title = "t post4",
                Body = "b post4",
                BodyOverview = "bo post4",
                Tags = "tg post4",
                MetaTitle = "mt post4",
                MetaKeywords = "mk post4",
                MetaDescription = "md post4",
                StartDateUtc = DateTime.UtcNow.AddHours(2),
                CreatedOnUtc = DateTime.UtcNow,
                EndDateUtc = DateTime.UtcNow.AddYears(5),
                AllowComments = true
            };
            var postContent = new StringContent(JsonConvert.SerializeObject(post), System.Text.Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken("user1", "123"));
            var httpResponse = await _client.PostAsync($"/{version}/Posts", postContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            //Assert
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);
        }

        #region Helpers

        protected async Task<string> GetToken(string userName, string password)
        {
            var user = new Models.AuthModels.LoginModel
            {
                UserName = userName,
                Password = password
            };
            var userContent = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync($"/{version}/auth/token", userContent);

            if (!httpResponse.IsSuccessStatusCode) return null;

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var tokenModel = JsonConvert.DeserializeObject<Models.AuthModels.TokenModel>(stringResponse);

            return tokenModel?.access_token;
        }

        protected string ToQueryString(object o)
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

        protected Dictionary<string, object> ToPropertyDictionary(object o)
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
