using System.Net.Http;

namespace Blog.API.IntegrationTests.Controllers.V11
{
    //https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
    public class AuthControllerTests : IntegrationTests.Controllers.V1.AuthControllerTests
    {
        public AuthControllerTests(BlogApiFactory<Startup> factory, string version = "v1.1") : base(factory, version)
        {
        }
    }
}
