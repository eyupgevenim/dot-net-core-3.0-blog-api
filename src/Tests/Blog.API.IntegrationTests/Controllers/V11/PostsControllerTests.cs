using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Blog.API.IntegrationTests.Controllers.V11
{
    //https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
    public class PostsControllerTests : IntegrationTests.Controllers.V1.PostsControllerTests
    {
        public PostsControllerTests(BlogApiFactory<Startup> factory, string version = "v1.1") : base(factory, version)
        {
        }
    }
}
