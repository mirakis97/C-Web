using Git.Data.Models;

namespace Git.Services
{
    public interface IUsersService
    {
        User CreateUser(string username, string email, string password);

        bool IsEmailAvailable(string email);

        string GetUserId(string username, string password);

        bool IsUsernameAvailable(string username);
    }
}
