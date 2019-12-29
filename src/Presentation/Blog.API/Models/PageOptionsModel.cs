using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models
{
    /// <summary>
    /// Page Options
    /// </summary>
    public abstract class PageOptionsModel
    {
        /// <summary>
        /// Page Number min:1 max:int.MaxValue
        /// </summary>
        [Range(1, int.MaxValue)]
        public virtual int Page { get; set; } = 1;

        /// <summary>
        /// Data Count min:1 max:20
        /// </summary>
        [Range(1, 20)]
        public virtual int Count { get; set; } = 10;
    }
}
