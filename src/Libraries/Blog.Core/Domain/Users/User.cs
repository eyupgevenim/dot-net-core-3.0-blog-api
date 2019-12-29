using System;

namespace Blog.Core.Domain.Users
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
