using Blog.API.Infrastructure.OperationFilters;
using Blog.API.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Blog.API.Infrastructure.Extensions
{
    /// <summary>
    /// Configure service extensions
    /// </summary>
    public static class ConfigureServiceExtensions
    {
        /// <summary>
        /// Custom api versioning
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);

                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
            return services;
        }

        /// <summary>
        /// Add JwtBearer Authentication configures
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(nameof(ApplicationOptions.Jwt)).Get<JwtOptions>();
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidAudience = jwtSettings.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey))
                    };
                });

            return services;
        }

        /// <summary>
        /// Adds Swagger services and configures the Swagger services.
        /// </summary>
        public static IServiceCollection AddSwaggerOptionService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var assembly = typeof(Startup).Assembly;
                var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

                options.DescribeAllParametersInCamelCase();

                options.OperationFilter<ApiVersionOperationFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                {
                    if (apiVersionDescription.IsDeprecated)
                        continue;

                    var info = new OpenApiInfo()
                    {
                        Title = assemblyProduct,
                        Description = apiVersionDescription.IsDeprecated
                        ? $"{assemblyDescription} This API version has been deprecated."
                        : assemblyDescription,
                        Version = apiVersionDescription.ApiVersion.ToString(),
                    };
                    options.SwaggerDoc(apiVersionDescription.GroupName, info);
                }

                //https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1295
                //https://stackoverflow.com/questions/58197244/swaggerui-with-netcore-3-0-bearer-token-authorization
                //options.OperationFilter<TokenOperationFilter>();
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                options.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                options.AddSecurityRequirement(securityRequirement);

            });

            return services;
        }
    }
}
