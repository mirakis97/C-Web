using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext dbContext;
        private readonly IUsersService usersService;

        public UsersController(
            IValidator validator,
            ApplicationDbContext dbContext,
            IPasswordHasher passwordHasher, IUsersService usersService)
        {
            this.validator = validator;
            this.dbContext = dbContext;
            this.usersService = usersService;
        }
        public HttpResponse Register()
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/Repositories/All");
            }

            return View();
        }
        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this.validator.ValidateUserRegistration(model);

            if (usersService.IsUsernameAvailable(model.Username))
            {
                modelErrors.Add($"User with '{model.Username}' username already exist!");
            }
            if (usersService.IsEmailAvailable(model.Email))
            {
                modelErrors.Add($"User with '{model.Email}' e-mail already exist!");
            }
            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            var user = usersService.CreateUser(model.Username,model.Email,model.Password);

            dbContext.Users.Add(user);

            dbContext.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/Repositories/All");
            }

            return View();
        }
        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            var userId = usersService.GetUserId(model.Username,model.Password);
            
            if (userId == null)
            {
                return Error("Username and pasword combination is not valid");
            }

            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
