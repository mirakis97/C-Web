using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services;
using CarShop.ViewModels.Issues;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IUserService userService;
        private readonly ApplicationDbContext dbContext;
        private readonly IValidator validator;

        public IssuesController(IUserService userService, ApplicationDbContext dbContext, IValidator validator)
        {
            this.userService = userService;
            this.dbContext = dbContext;
            this.validator = validator;
        }
        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            if (!this.userService.IsMechanic(this.User.Id))
            {
                var userOwnsCar = this.dbContext.Cars.Any(c => c.Id == carId && c.OwnerId == this.User.Id);

                if (!userOwnsCar)
                {
                    return Error("You do not have access to this car.");
                }
            }
            var carIssues = this.dbContext.Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarIssuesViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    Issues = c.Issues.Select(i => new IssuesListingViewModel
                    {
                        Id = i.Id,
                        Description = i.Description,
                        IsFixed = i.IsFixed
                    })
                }).FirstOrDefault();
            if (carIssues == null)
            {
                return Error($"Car with Id '{carId} does not exist!'");
            }
            return View(carIssues);
        }
        [Authorize]
        public HttpResponse Add(string carId) => View(new AddIssueViewModel
        {
            CarId = carId,
        });

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddIssueFormModel model)
        {
            var modelErrors = this.validator.IsValidIssueForm(model);

            if (modelErrors.Any())
            {
                return View("./Shared/Error", modelErrors);
            }

            var issue = new Issue
            {
                Description = model.Description,
                CarId = model.CarId
            };

            dbContext.Issues.Add(issue);
            dbContext.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={model.CarId}");
        }

        [Authorize]
        public HttpResponse Fix(string issueId, string carId)
        {
            var user = this.dbContext
                .Users
                .Where(u => u.Id == this.User.Id)
                .FirstOrDefault();

            if (!user.IsMechanic)
            {
                return Unauthorized();
            }

            var issue = this.dbContext.Issues.Find(issueId);

            issue.IsFixed = true;
            this.dbContext.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        [Authorize]
        public HttpResponse Delete(string issueId, string carId)
        {
            var issue = this.dbContext.Issues.Find(issueId);

            this.dbContext.Issues.Remove(issue);

            this.dbContext.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
