using Blog.API.Models.AuthModels;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Blog.API.IntegrationTests.Controllers.V1
{
    //https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
    public class AuthControllerTests : IClassFixture<BlogApiFactory<Startup>>
    {
        protected string version { get; }

        protected readonly HttpClient _client;

        public AuthControllerTests(BlogApiFactory<Startup> factory, string version = "v1")
        {
            this.version = version;
            _client = factory.CreateClient();
        }

        [Fact]
        public virtual async Task Get_Token_Authorized()
        {
            // Arrange

            var user = new LoginModel
            {
                UserName = "user1",
                Password = "123"
            };
            var userContent = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync($"/{version}/auth/token", userContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var tokenModel = JsonConvert.DeserializeObject<TokenModel>(stringResponse);

            //Assert

            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(tokenModel.access_token);
            Assert.True(tokenModel.expires_in > 0);
            Assert.True(tokenModel.token_type == "bearer");
        }

        [Fact]
        public virtual async Task Get_Token_Unauthorized()
        {
            // Arrange

            var user = new LoginModel
            {
                UserName = "user1",
                Password = "111"
            };
            var userContent = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync($"/{version}/auth/token", userContent);

            // Must be error.
            //httpResponse.EnsureSuccessStatusCode();


            //Assert

            Assert.Throws<HttpRequestException>(() => httpResponse.EnsureSuccessStatusCode());

            Assert.True(httpResponse.StatusCode == HttpStatusCode.Unauthorized);

        }

    }
}
