using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.PostModels
{
    /// <summary>
    /// post search model options
    /// </summary>
    public class PostPageOptionsModel : PageOptionsModel
    {
        /// <summary>
        /// post title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// post body
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Data Count min:1 max:100
        /// </summary>
        [Range(1, 100)]
        public override int Count { get; set; } = 100;
    }
}
