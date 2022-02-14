using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Data;
using SMS.Data.Models;
using SMS.Models.Products;
using SMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IValidator validator;
        private readonly SMSDbContext dbContext;
        public ProductsController(Request request, SMSDbContext dbContext, IValidator validator) : base(request)
        {
            this.dbContext = dbContext;
            this.validator = validator;
        }
        [Authorize]
        public Response Create()
        {
            return View(new  { IsAuthenticated = true});
        }
        [Authorize]
        [HttpPost]
        public Response Create(CreateViewModel model)
        {
            var (isValid, error) = validator.ValidateProductCreateModel(model);
            if (!isValid)
            {
                return View(new { ErrorMessage = error }, "/Error");
            }
            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price
            };
            try
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                return View("Unexpected error", "/Error");
            }

            return Redirect("/");
        }
    }
}
