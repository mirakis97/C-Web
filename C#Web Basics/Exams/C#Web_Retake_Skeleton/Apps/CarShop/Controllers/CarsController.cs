using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services;
using CarShop.ViewModels.Car;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext dbContext;
        public CarsController(IValidator validator,ApplicationDbContext dbContext)
        {
            this.validator = validator;
            this.dbContext = dbContext;
        }
        public HttpResponse Add() => View();
        [HttpPost]
        public HttpResponse Add(AddCarFormModel model)
        {
            var modelErrors = this.validator.ValidateCarCreation(model);

            if (modelErrors.Any())
            {
                var sb = new StringBuilder();
                foreach (var error in modelErrors)
                {
                    sb.AppendLine(error);
                }
                return Error(sb.ToString());
            }
            var car = new Car
            {

            };
        }
        public HttpResponse All()
        {
            return View();
        }
    }
}
