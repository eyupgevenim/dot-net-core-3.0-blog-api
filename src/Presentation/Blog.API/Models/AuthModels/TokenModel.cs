namespace Blog.API.Models.AuthModels
{
    /// <summary>
    /// Token result model
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Token data
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// Token type
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// token expires
        /// </summary>
        public int expires_in { get; set; }
    }
}
