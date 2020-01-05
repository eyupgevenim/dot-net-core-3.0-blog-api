using Blog.API.Infrastructure.Options;
using Blog.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace Blog.API.Controllers.V11
{
    /// <summary>
    /// Auth api controller v1.1
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Route("v1.1/auth")]
    [ApiController]
    public class AuthController : V1.AuthController//BaseV11Controller
    {
        public AuthController(ISignInManager _signInManager, 
            IOptions<JwtOptions> _jwtStrings)
            : base(_signInManager, _jwtStrings)
        {
        }
    }
}
