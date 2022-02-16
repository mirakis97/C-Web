using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Commits;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IValidator validator;

        public CommitsController(ApplicationDbContext dbContext, IValidator validator)
        {
            this.dbContext = dbContext;
            this.validator = validator;
        }
        [Authorize]
        public HttpResponse Create(string id)
        {
            var repository = this.dbContext
                .Repositories
                .Where(r => r.Id == id)
                .Select(r => new CommitToRepositoryViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .FirstOrDefault();

            if (repository == null)
            {
                return BadRequest();
            }

            return View(repository);
        }
        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateCommitFormModel model)
        {
            var modelErrors = this.validator.ValidateCreateComit(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            var commit = new Commit
            {
                RepositoryId = model.Id,
                Description = model.Description,
                CreatorId = User.Id,
                CreatedOn = DateTime.Now
            };

            this.dbContext.Commits.Add(commit);

            dbContext.SaveChanges();

            return Redirect("/Repositories/All");
        }
        [Authorize]
        public HttpResponse All()
        {
            List<AllComitsListModel> repository;
            repository = this.dbContext
                 .Commits
                 .Where(c => c.CreatorId == User.Id)
                 .OrderByDescending(c => c.CreatedOn)
                 .Select(c => new AllComitsListModel
                 {
                     Id = c.Id,
                     Description = c.Description,
                     CreatedOn = c.CreatedOn.ToLocalTime().ToString("F"),
                     Repository = c.Repository.Name
                 })
                 .ToList();

            return View(repository);
        }
        [Authorize]
        public HttpResponse Delete(string id)
        {

            var commit = dbContext.Commits
                .FirstOrDefault(x => x.Id == id);

            string error = string.Empty;

            if (commit == null || commit.CreatorId != id)
            {
                error = "Bad Request";
                return Error(error);
            }

            this.dbContext.Commits.Remove(commit);

            this.dbContext.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
}