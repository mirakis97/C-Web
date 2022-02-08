using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services;
using CarShop.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly ApplicationDbContext dbContext;

        public UsersController(
            IValidator validator, 
            ApplicationDbContext dbContext,
            IPasswordHasher passwordHasher)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.dbContext = dbContext;

        }
        public HttpResponse Register() => View();
        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this.validator.ValidateUserRegistration(model);

            if (this.dbContext.Users.Any(u => u.Username == model.Username))
            {
                modelErrors.Add($"User with '{model.Username}' username already exist!");
            }
            if (this.dbContext.Users.Any(u => u.Email == model.Email))
            {
                modelErrors.Add($"User with '{model.Email}' e-mail already exist!");
            }
            if (modelErrors.Any())
            {
                var sb = new StringBuilder();
                foreach (var error in modelErrors)
                {
                    sb.AppendLine(error);
                }
                return Error(sb.ToString());
            }
            var user = new User
            {
                Username = model.Username,
                Password = this.passwordHasher.Hash(model.Password),
                Email = model.Email,
                IsMechanic = model.UserType == "Mechanic",
            };

            dbContext.Users.Add(user);

            dbContext.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login() => View();
        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            var hashedPasword = this.passwordHasher.Hash(model.Password);
            var userId = this.dbContext.Users
                .Where(u => u.Username == model.Username && u.Password == hashedPasword)
                .Select(u => u.Id)
                .FirstOrDefault();
            if (userId == null)
            {
                return Error("Username and pasword combination is not valid");
            }

            this.SignIn(userId);

            return Redirect("/Car/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}