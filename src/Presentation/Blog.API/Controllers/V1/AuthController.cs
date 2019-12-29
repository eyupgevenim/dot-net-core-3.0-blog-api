using Blog.API.Infrastructure.Options;
using Blog.API.Models.AuthModels;
using Blog.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Blog.API.Controllers.V1
{
    /// <summary>
    /// Auth api controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v1/auth")]
    [ApiController]
    public class AuthController : Base.BaseV1Controller
    {
        protected readonly ISignInManager _signInManager;
        protected readonly IOptions<JwtOptions> _jwtStrings;
        public AuthController(ISignInManager _signInManager, 
            IOptions<JwtOptions> _jwtStrings)
        {
            this._signInManager = _signInManager;
            this._jwtStrings = _jwtStrings;
        }

        /// <summary>
        /// Get token
        /// </summary>
        /// <param name="user">LoginModel</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("token")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Return random a token key")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid client request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        public virtual IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
                return BadRequest("Invalid client request");

            if(_signInManager.SignIn(user.UserName, user.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtStrings.Value.IssuerSigningKey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: _jwtStrings.Value.ValidIssuer,
                    audience: _jwtStrings.Value.ValidAudience,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(_jwtStrings.Value.Expires),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new TokenModel 
                { 
                    access_token = tokenString, 
                    token_type = "bearer",
                    expires_in = _jwtStrings.Value.Expires 
                });
            }

            return Unauthorized();
        }
    }
}
