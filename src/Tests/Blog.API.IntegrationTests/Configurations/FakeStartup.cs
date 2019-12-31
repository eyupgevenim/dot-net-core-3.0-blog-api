using Blog.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.API.IntegrationTests.Configurations
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<IDbContext>();

                if (!dbContext.ConnectionString.ToLower().Contains("test.connection.strings"))
                {
                    throw new Exception("LIVE SETTINGS IN TESTS!");
                }

                // Initialize database
            }
        }
    }
}
