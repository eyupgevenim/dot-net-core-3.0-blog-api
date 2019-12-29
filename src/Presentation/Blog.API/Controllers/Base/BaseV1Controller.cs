using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Blog.API.Controllers.Base
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    [ApiController]
    public abstract class BaseV1Controller : ControllerBase
    {
        protected virtual void LogInformation<LoggerType>(string message, params object[] args)
        {
            var log = HttpContext.RequestServices.GetService<ILogger<LoggerType>>();
            if (args.Length > 0)
            {
                log.LogInformation(message, args);
            }
            else
            {
                log.LogInformation(message);
            }
        }

        protected virtual void LogError<LoggerType>(Exception exception, string message = null, params object[] args)
        {
            var log = HttpContext.RequestServices.GetService<ILogger<BaseV1Controller>>();
            if (args.Length > 0)
            {
                log.LogError(exception, message ?? exception.Message, args);
            }
            else
            {
                log.LogError(exception, message ?? exception.Message);
            }
        }
    }
}
