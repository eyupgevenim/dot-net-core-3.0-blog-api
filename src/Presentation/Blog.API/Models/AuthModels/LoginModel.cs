using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.AuthModels
{
    /// <summary>
    /// Login model for token 
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
