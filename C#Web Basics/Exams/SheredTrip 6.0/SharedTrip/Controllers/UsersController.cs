﻿using BasicWebServer.Server.Attributes;
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
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;
        public UsersController(Request request, IUserService userService, IValidator validator, ApplicationDbContext dbContext, IPasswordHasher passwordHasher)
            : base(request)
        {
            this.userService = userService;
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }

        public Response Login()
            => View();
        public Response Register()
            => View();
        [HttpPost]
        public Response Register(RegisterViewModel model)
        {
            var (isValid, errors) = userService.ValidateModel(model);
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

        public Response Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}