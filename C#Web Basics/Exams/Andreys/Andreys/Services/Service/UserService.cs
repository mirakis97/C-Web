using Andreys.Data;
using Andreys.Data.Models;
using Andreys.Services.Contracts;
using Andreys.ViewModels.Users;
using System.Collections.Generic;
using System.Linq;

namespace Andreys.Services.Service
{
    public class UserService : BaseService<User>,IUserService
    {
        private readonly IPasswordHasher passwordHasher;
        public UserService(AndreysDbContext data, IValidatorService validator, IPasswordHasher passwordHasher) : base(data, validator)
        {
            this.passwordHasher = passwordHasher;
        }

        public List<string> CreateUser(RegisterFormViewModel model)
        {
            ICollection<string> modelErrors = Validator.ValidateModel(model);

            if (!IsEmailAvailable(model.email))
            {
                modelErrors.Add($"User with '{model.email}' e-mail already exists.");
            }

            if (!IsUsernameAvailable(model.username))
            {
                modelErrors.Add($"User with '{model.username}' username already exists.");

            }
            if (modelErrors.Count != 0)
            {
                return modelErrors.ToList();
            }

            var user = new User
            {
                Username = model.username,
                Password = passwordHasher.GeneratePassword(model.password),
                Email = model.email
            };

            Data.Users.Add(user);
            Data.SaveChanges();

            return modelErrors.ToList();
        }

        public bool IsEmailAvailable(string email)
        {
            if (Data.Users.Any(u => u.Email == email))
            {
                return false;
            }

            return true;
        }

        public bool IsUsernameAvailable(string username)
        {
            if (Data.Users.Any(u => u.Username == username))
            {
                return false;
            }

            return true;
        }

        public (string userId, string error) Login(LoginForemViewModel model)
        {
            var hashedPassword = passwordHasher.GeneratePassword(model.Password);
            string modelErrors = string.Empty;

            var userId = Data
                .Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                modelErrors = "Wrong Login Information, UserName or Password are incorect";
            }

            return (userId, modelErrors);
        }
    }
}
