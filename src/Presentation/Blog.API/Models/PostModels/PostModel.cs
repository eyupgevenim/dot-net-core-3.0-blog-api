using System;
using System.ComponentModel;

namespace Blog.API.Models.PostModels
{
    /// <summary>
    /// Post model
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// post title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// post body
        /// </summary>
        public string Body { get; set; }

        //..
    }
}
