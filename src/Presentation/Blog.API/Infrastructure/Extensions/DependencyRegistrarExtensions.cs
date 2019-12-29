using Blog.API.Infrastructure.Options;
using Blog.Data;
using Blog.Services.Posts;
using Blog.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.API.Infrastructure.Extensions
{
    /// <summary>
    /// Registrar dependency injection extensions
    /// </summary>
    public static class DependencyRegistrarExtensions
    {
        /// <summary>
        /// Register dependency injection
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<ApplicationOptions>(configuration)
                .Configure<ConnectionStringsOptions>(configuration.GetSection(nameof(ApplicationOptions.ConnectionStrings)))
                .Configure<JwtOptions>(configuration.GetSection(nameof(ApplicationOptions.Jwt)));

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ISignInManager, SignInManager>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDatabaseContext(configuration);

            return services;
        }

        /// <summary>
        /// Ragestir database context
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection(nameof(ApplicationOptions.ConnectionStrings)).Get<ConnectionStringsOptions>();
            services.AddDbContext<IDbContext, AppDbContext>(options =>
            {
                options.UseSqlServer(connectionStrings.DefaultConnection);
            });
            ////services.AddSingleton<IDbContext, AppDbContext>();
            
            return services;
        }
    }
}
