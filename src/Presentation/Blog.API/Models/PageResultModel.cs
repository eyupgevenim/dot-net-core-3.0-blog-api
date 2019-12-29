using System.Collections.Generic;

namespace Blog.API.Models
{
    /// <summary>
    /// Paging generic model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResultModel<T> where T : class
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Page data count
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Has next page
        /// </summary>
        public bool HasNextPage { get => this.Page < this.TotalPages; }
        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPreviousPage { get => this.Page > 1; }
        /// <summary>
        /// Total data
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Date items
        /// </summary>
        public List<T> Items { get; set; }
    }
}
