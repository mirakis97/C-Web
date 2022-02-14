using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Controllers
{

     public class CartsController : Controller
    {
        private readonly IValidator validator;
        public CartsController(Request request, IValidator validator) : base(request)
        {
            this.validator = validator;
        }
        [Authorize]
        public Response Details()
        {
            var products = validator.GetUserProducts(User.Id);
            
            return this.View(new 
            { 
                products = products,
                IsAuthenticated = true 
            });
        }
        [Authorize]
        public Response AddProduct(string productId)
        {
            var products = validator.AddProduct(productId, User.Id);

            return View(new 
            { 
                products = products,
                IsAuthenticated = true,
            },"/Carts/Details");
        }
        [Authorize]
        public Response Buy()
        {
            validator.BuyProducts(User.Id);

            return Redirect("/");
        }
    }
}
