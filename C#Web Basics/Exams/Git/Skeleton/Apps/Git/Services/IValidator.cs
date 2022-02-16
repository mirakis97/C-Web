using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using Git.ViewModels.Users;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserFormModel model);

        ICollection<string> ValidateCreateModel(CreateRepositoryModelView car);
        ICollection<string> ValidateCreateComit(CreateCommitFormModel model);
    }
}