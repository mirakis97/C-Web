using Andreys.ViewModels.Users;
using System.Collections.Generic;

namespace Andreys.Services.Contracts
{
    public interface IUserService
    {
        List<string> CreateUser(RegisterFormViewModel model);

        (string userId, string error) Login(LoginForemViewModel model);

        bool IsEmailAvailable(string email);

        bool IsUsernameAvailable(string username);
    }
}
