using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers.Base
{
    [Authorize]
    [ApiVersion("1.1")]//[ApiVersion("1.1", Deprecated = true)]
    [Route("v1.1/[controller]")]
    [ApiController]
    public abstract class BaseV11Controller : BaseV1Controller
    {
    }
}
