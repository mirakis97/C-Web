using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using Git.ViewModels.Users;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Git.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCreateComit(CreateCommitFormModel model)
        {
            var errors = new List<string>();
            if (model.Description.Length < 5)
            {
                errors.Add("Repository must be between 3 and 10 elements long!");
            }

            return errors;
        }

        public ICollection<string> ValidateCreateModel(CreateRepositoryModelView model)
        {
            var errors = new List<string>();
            if (model.Name.Length < 3 || model.Name.Length > 10)
            {
                errors.Add("Repository must be between 3 and 10 elements long!");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(RegisterUserFormModel model)
        {
            var errors = new List<string>();
            if (model.Username.Length < 5 || model.Username.Length > 20 )
            {
                errors.Add("Car model must be between 5 and 20 elements long!");
            }
            if(model.Password.Length < 6 || model.Password.Length > 20)
            {
                errors.Add("Password length must be between 6 and 20 sybmols!");
            }
            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Passwords and its confimation do not match!");
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                errors.Add("Email is required!");
            }
            return errors;
        }
    }
}
