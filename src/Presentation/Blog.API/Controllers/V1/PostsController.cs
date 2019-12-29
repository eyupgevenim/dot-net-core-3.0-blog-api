using System;
using System.Linq;
using Blog.API.Models.PostModels;
using Blog.Core.Domain.Posts;
using Blog.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Blog.API.Controllers.V1
{
    /// <summary>
    /// Posts api controller
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    [ApiController]
    public class PostsController : Base.BaseV1Controller
    {
        protected readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// search posts PostPageOptionsModel model parameter
        /// </summary>
        /// <param name="pageOptions">search parameters</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous()]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of Posts for the specified page.", typeof(PostPageResultModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public virtual IActionResult Get([FromQuery] PostPageOptionsModel pageOptions)
        {
            try
            {
                var result = _postService.SearchPosts(pageOptions.Title, body: pageOptions.Body, pageIndex: (pageOptions.Page - 1), pageSize: pageOptions.Count);
                if (result.TotalCount == 0)
                    return new NotFoundResult();

                var model = new PostPageResultModel
                {
                    TotalCount = result.TotalCount,
                    TotalPages = result.TotalPages,
                    Page = pageOptions.Page,
                    Count = result.Items.Count,
                    Items = result.Items.Select(x => new PostModel
                    {
                        Title = x.Title,
                        Body = x.Body,
                        BodyOverview = x.BodyOverview,
                        MetaTitle = x.MetaTitle,
                        MetaDescription = x.MetaDescription,
                        MetaKeywords = x.MetaKeywords,
                        Tags = x.Tags,
                        AllowComments = x.AllowComments,
                        CreatedOnUtc = x.CreatedOnUtc,
                        StartDateUtc = x.StartDateUtc,
                        EndDateUtc = x.EndDateUtc
                    }).ToList()
                };

                return new ObjectResult(model);
            }
            catch (Exception ex)
            {
                base.LogError<PostsController>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous()]
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public virtual IActionResult Get(int id)
        {
            try
            {
                var entity = _postService.GetPostById(id);
                if (entity == null)
                    return new NotFoundResult();

                var model = new PostModel
                {
                    Title = entity.Title,
                    Body = entity.Body,
                    BodyOverview = entity.BodyOverview,
                    MetaTitle = entity.MetaTitle,
                    MetaDescription = entity.MetaDescription,
                    MetaKeywords = entity.MetaKeywords,
                    Tags = entity.Tags,
                    CreatedOnUtc = entity.CreatedOnUtc,
                    AllowComments = entity.AllowComments,
                    StartDateUtc = entity.StartDateUtc,
                    EndDateUtc = entity.EndDateUtc
                };

                return new ObjectResult(model);
            }
            catch (Exception ex)
            {
                base.LogError<PostsController>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add post as PostModel model
        /// </summary>
        /// <param name="model">PostModel model</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Insert success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public virtual IActionResult Post([FromBody] PostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new Post
                    {
                        Title = model.Title,
                        Body = model.Body,
                        BodyOverview = model.BodyOverview,
                        MetaTitle = model.MetaTitle,
                        MetaDescription = model.MetaDescription,
                        MetaKeywords = model.MetaKeywords,
                        Tags = model.Tags,
                        AllowComments = model.AllowComments,
                        StartDateUtc = model.StartDateUtc,
                        EndDateUtc = model.EndDateUtc
                    };
                    _postService.AddPost(entity);

                    return Ok();
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                base.LogError<PostsController>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update post by id
        /// </summary>
        /// <param name="id">post id</param>
        /// <param name="model">PostModel model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public virtual IActionResult Put(int id, [FromBody]PostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _postService.GetPostById(id);
                    if (entity == null)
                        return new NotFoundResult();

                    entity.Title = model.Title;
                    entity.Body = model.Body;
                    entity.BodyOverview = model.BodyOverview;
                    entity.AllowComments = model.AllowComments;
                    entity.Tags = model.Tags;
                    entity.MetaDescription = model.MetaDescription;
                    entity.MetaKeywords = model.MetaKeywords;
                    entity.MetaTitle = model.MetaTitle;
                    entity.StartDateUtc = model.StartDateUtc;
                    entity.EndDateUtc = model.EndDateUtc;
                    _postService.UpdatePost(entity);

                    return Ok();
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                base.LogError<PostsController>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                var entity = _postService.GetPostById(id);
                if (entity == null)
                    return new NotFoundResult();

                entity.Deleted = true;
                _postService.UpdatePost(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                base.LogError<PostsController>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
