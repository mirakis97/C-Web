using Andreys.Services.Contracts;
using Andreys.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;

namespace Andreys.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(
            IUserService _userService)
        {
            userService = _userService;
        }

        public HttpResponse Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginForemViewModel model)
        {
            (string userId, string error) = userService.Login(model);

            if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId))
            {
                return Error(error);
            }

            SignIn(userId);

            return Redirect("/");
        }

        public HttpResponse Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterFormViewModel model)
        {
            var result = userService.CreateUser(model);

            if (result.Count != 0)
            {
                return Error(result);
            }

            return Redirect("/Users/Login");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            SignOut();

            return Redirect("/");
        }
    }
}
