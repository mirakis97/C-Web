using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Repositories;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IValidator validator;

        public RepositoriesController(ApplicationDbContext dbContext, IValidator validator)
        {
            this.dbContext = dbContext;
            this.validator = validator;
        }
        public HttpResponse Create() => View();
        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateRepositoryModelView model)
        {
            var modelErrors = this.validator.ValidateCreateModel(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            var repo = new Repository
            {
                Name = model.Name,
                IsPublic = model.RepositoryType == "Public" ? true : false,
                CreatedOn = DateTime.Now,
                OwnerId = User.Id
            };

            this.dbContext.Repositories.Add(repo);

            dbContext.SaveChanges();

            return Redirect("/Repositories/All");
        }
        [Authorize]
        public HttpResponse All()
        {
            List<AllRepositoryListModel> repository;
            if (User.Id != null)
            {
               dbContext.Repositories.Where(x => x.IsPublic || x.OwnerId == User.Id);
            }
            else
            {
              dbContext.Repositories.Where(x => x.IsPublic).ToList();
            }
            repository = this.dbContext
                    .Repositories
                    .OrderByDescending(x => x.CreatedOn)
                    .Select(c => new AllRepositoryListModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Owner = c.Owner.Username,
                        CreatedOn = c.CreatedOn.ToLocalTime().ToString("F"),
                        Commits = c.Commits.Count()
                    }).ToList();
            return View(repository);
        }
    }
}
