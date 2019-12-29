using Blog.Core.Domain.Users;

namespace Blog.Services.Users
{
    public interface ISignInManager
    {
        bool SignIn(string userName, string password);
        User CreatePassword(User user, string password);
    }
}
