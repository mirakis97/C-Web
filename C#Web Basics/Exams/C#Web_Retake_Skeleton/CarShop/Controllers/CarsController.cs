using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services;
using CarShop.ViewModels.Car;
using MyWebServer.Controllers;
using MyWebServer.Http;
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
        public CarsController(IValidator validator, ApplicationDbContext dbContext)
        {
            this.validator = validator;
            this.dbContext = dbContext;
        }
        [Authorize]
        public HttpResponse Add()
        {
            if (this.IsUserMechanic())
            {
                return Unauthorized();
            }

            return View();
        }
        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddCarFormModel model)
        {
            if (this.IsUserMechanic())
            {
                return Unauthorized();
            }
            var modelErrors = this.validator.ValidateCarCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            var car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id,
            };

            this.dbContext.Cars.Add(car);

            dbContext.SaveChanges();

            return Redirect("/Cars/All");
        }
        [Authorize]
        public HttpResponse All()
        {
            List<AllListedCarModel> cars;
            if (this.IsUserMechanic())
            {
                cars = this.dbContext
                    .Cars
                    .Where(c => c.Issues.Any(i => !i.IsFixed)).Select(c => new AllListedCarModel
                    {
                        Id = c.Id,
                        Model = c.Model,
                        Year = c.Year,
                        Image = c.PictureUrl,
                        PlateNumber = c.PlateNumber,
                        FixedIssues = c.Issues.Where(i => i.IsFixed).Count(),
                        RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count(),
                    }).ToList();
            }
            else
            {
                cars = this.dbContext
                    .Cars.Where(c => c.OwnerId == this.User.Id)
                    .Select(c => new AllListedCarModel
                    {
                        Id = c.Id,
                        Model = c.Model,
                        Year = c.Year,
                        Image = c.PictureUrl,
                        PlateNumber = c.PlateNumber,
                        FixedIssues = c.Issues.Where(i => i.IsFixed).Count(),
                        RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count(),
                    }).ToList();
            }
            return View(cars);
        }

        private bool IsUserMechanic()
            => this.dbContext.Users.Any(u => u.Id == this.User.Id && u.IsMechanic);
    }
}
