using Blog.Core.Domain.Users;
using Blog.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Services.Users
{
    public class SignInManager : ISignInManager
    {
        private readonly IRepository<User> _repositoryUser;
        public SignInManager(IRepository<User> _repositoryUser)
        {
            this._repositoryUser = _repositoryUser;
        }

        public bool SignIn(string userName, string password)
        {
            var user = _repositoryUser.Table.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
                return false;

            if (!CheckHashedPassword(user, password))
                return false;

            return true;
        }

        public User CreatePassword(User user, string password)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            user.Salt = CreateSalt();
            user.HashedPassword = HashedPassword(password, user.Salt);

            return user;
        }

        #region Helpers

        private bool CheckHashedPassword(User user, string password)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            if (user.HashedPassword != HashedPassword(password, user.Salt))
                return false;

            return true;
        }

        private string HashedPassword(string password, string salt)
        {
            return Convert.ToBase64String(
                    KeyDerivation
                    .Pbkdf2(
                        password,
                        Encoding.UTF8.GetBytes(salt),
                        KeyDerivationPrf.HMACSHA1,
                        10000,
                        256 / 8
                    )
            );
        }

        private string CreateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        #endregion
    }
}
