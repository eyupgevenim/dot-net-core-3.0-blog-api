using Blog.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers.V11
{
    /// <summary>
    /// Posts api controller v1.1
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    //[Route("v{version:apiVersion}/[controller]")]
    [Route("v1.1/[controller]")]
    [ApiController]
    public class PostsController : V1.PostsController//BaseV11Controller
    {
        public PostsController(IPostService postService):base(postService)
        {
        }

        /// <summary>
        /// Override delete method - v1.1
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("test/{id}")]
        public override IActionResult Delete(int id)
        {
            //some code
            //TODO:...

            return base.Delete(id);
        }
    }
}
