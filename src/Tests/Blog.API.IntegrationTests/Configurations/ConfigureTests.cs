using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Blog.API.IntegrationTests.Configurations
{
    public class ConfigureTests
    {
        [Fact]
        public void ConfigureServices_RegistersDependenciesCorrectly()
        {
            var connectionStrings = new Dictionary<string, string>
            {
                ["ConnectionStrings:DefaultConnection"] = "test.connection.strings",
            };
            var jwt = new Dictionary<string, string> 
            {
                ["Jwt:IssuerSigningKey"] = "testSuperSecretKey@365", 
                ["Jwt:ValidIssuer"] = "http://localhost:1111",
                ["Jwt:ValidAudience"] = "http://localhost:1111",
                ["Jwt:Expires"] = "300"
            };

            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(connectionStrings);
            builder.AddInMemoryCollection(jwt);
            var configuration = (IConfiguration)builder.Build();

            var services = new ServiceCollection();
            var target = new FakeStartup(configuration);
            target.ConfigureServices(services);
            services.AddTransient<Controllers.V1.PostsController>();

            var serviceProvider = services.BuildServiceProvider();
            var controller = serviceProvider.GetService<Controllers.V1.PostsController>();

            Assert.NotNull(controller);
        }
    }
}
