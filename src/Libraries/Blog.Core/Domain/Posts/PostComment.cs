using System;

namespace Blog.Core.Domain.Posts
{
    public class PostComment : BaseEntity
    {
        /// <summary>
        /// Gets or sets the comment text
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the comment is approved
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the blog post identifier
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the blog post
        /// </summary>
        public virtual Post Post { get; set; }
    }
}
