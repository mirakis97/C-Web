using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using CarShop.Services;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using SharedTrip.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedTrip.Controllers
{

    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;
        private readonly IValidator validator;
        public UsersController(Request request,IValidator validator, ApplicationDbContext dbContext, IPasswordHasher passwordHasher)
            : base(request)
        {
            this.dbContext = dbContext;
            this.validator = validator;
            this.passwordHasher = passwordHasher;
        }

        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/Trips/All");
            }
            return this.View();
        }
        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/Trips/All");
            }
            return this.View();
        }
        [HttpPost]
        public Response Register(RegisterViewModel model)
        {
            var (isValid, errors) = validator.ValidateRegisterModel(model);
            if (this.dbContext.Users.Any(u => u.Username == model.Username))
            {
                isValid = false;
                errors.Add(new ErrorViewModel($"User with '{model.Username}' username already exist!"));
            }
            if (this.dbContext.Users.Any(u => u.Email == model.Email))
            {
                isValid = false;
                errors.Add(new ErrorViewModel($"User with '{model.Email}' e-mail already exist!"));
            }
            if (!isValid)
            {
                return View(errors, "/Error");
            }

            try
            {
                User user = new User
                {
                    Email = model.Email,
                    Username = model.Username,
                    Password = this.passwordHasher.Hash(model.Password),
                };

                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                return View(new List<ErrorViewModel>() { new ErrorViewModel("Unexpected Error") }, "/Error");
            }

            return Redirect("/Users/Login");
        }
        [HttpPost]
        public Response Login(LoginFormModel model)
        {
            var hashedPasword = this.passwordHasher.Hash(model.Password);
            var userId = this.dbContext.Users
                .Where(u => u.Username == model.Username && u.Password == hashedPasword)
                .Select(u => u.Id)
                .FirstOrDefault();
            if (userId == null)
            {
                return View(new List<ErrorViewModel>() { new ErrorViewModel("Login incorrect!") }, "/Error");
            }

            this.SignIn(userId);

            return Redirect("/Trips/All");
        }
        [Authorize]
        public Response Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}