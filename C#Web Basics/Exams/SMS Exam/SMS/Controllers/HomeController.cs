using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Models.Products;
using SMS.Services;
using System.Collections.Generic;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValidator validator;
        public HomeController(
            Request request, 
            IValidator validator)
            : base(request)
        {
            this.validator = validator;
        }

        public Response Index()
        {
            if (User.IsAuthenticated)
            {
                string username = validator.GetUsername(User.Id);
                var model = new
                {
                    Username = username,
                    IsAuthenticated = true,
                    Products = validator.GetProducts()
                };

                return View(model, "/Home/IndexLoggedIn");
            }
            return this.View(new { IsAuthenticated = false });
        }
    }
}