using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Blog.API.Infrastructure.OperationFilters
{
    /// <summary>
    /// https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1295
    /// //https://stackoverflow.com/questions/58197244/swaggerui-with-netcore-3-0-bearer-token-authorization
    /// </summary>
    public class TokenOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            /**
            var filterDescriptors = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            bool allowAnonymous = filterDescriptors.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);
            var isAuthorized = filterDescriptors.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter)
            //if (!isAuthorized && !allowAnonymous)
            //    return;
            */

            var allowAnonymous = context.MethodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(AllowAnonymousAttribute));

            if (allowAnonymous)
                return;

            if (operation == null)
                return;

            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Description = "access token",
                Name = "Authorization",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("Bearer {access token}"),
                }
            });
        }
    }
}
