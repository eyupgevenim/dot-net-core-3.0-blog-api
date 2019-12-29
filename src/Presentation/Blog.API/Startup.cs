using Blog.API.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Blog.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDependencyInjection(Configuration);
            services.AddSwaggerOptionService();
            services.AddCustomApiVersioning();
            services.AddJwtBearerAuthentication(Configuration);
            services.AddAuthorization();
            services
                .AddMvcCore()
                .AddApiExplorer()
                .AddDataAnnotations();
            services.AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add this line; you'll need `using Serilog;` up the top, too
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseCustomSwaggerUI();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
