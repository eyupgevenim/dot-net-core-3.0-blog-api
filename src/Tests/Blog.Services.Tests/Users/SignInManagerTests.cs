using Blog.Core.Domain.Users;
using Blog.Data;
using Blog.Services.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blog.Services.Tests.Users
{
    public class SignInManagerTests
    {
        private ISignInManager _signInManager;
        private Mock<IRepository<User>> _repositoryUser;

        public SignInManagerTests()
        {
            _repositoryUser = new Mock<IRepository<User>>();
            _signInManager = new SignInManager(_repositoryUser.Object);
        }

        [Fact]
        public void create_user_hashed_password_and_salt()
        {
            var user = new User
            {
                FirstName = "fn test",
                LastName = "ln test",
                UserName = "user_test",
                Email = "user@test.com",
                Active = true,
                CreatedOnUtc = DateTime.UtcNow
            };
            _signInManager.CreatePassword(user, "123");
            Assert.NotEmpty(user.HashedPassword);
            Assert.NotEmpty(user.Salt);
        }

        [Fact]
        public void user_sign_in()
        {
            var user = new User
            {
                FirstName = "fn test",
                LastName = "ln test",
                UserName = "user_test",
                Email = "user@test.com",
                Active = true,
                CreatedOnUtc = DateTime.UtcNow
            };
            _signInManager.CreatePassword(user, "123");
            _repositoryUser.Setup(x => x.Table).Returns(new List<User> { user }.AsQueryable());
            _signInManager = new SignInManager(_repositoryUser.Object);

            Assert.True(_signInManager.SignIn("user_test", "123"));
        }
    }
}
