using Git.Data;
using Git.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;
        public UsersService(ApplicationDbContext dbContext, IPasswordHasher passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }
        public User CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Password = this.passwordHasher.Hash(password),
                Email = email
            };

            return user;
        }

        public string GetUserId(string username, string password)
        {
            var hashedPasword = this.passwordHasher.Hash(password);
            var userId = this.dbContext.Users
                .Where(u => u.Username == username && u.Password == hashedPasword)
                .Select(u => u.Id)
                .FirstOrDefault();

            return userId;
        }

        public bool IsEmailAvailable(string email)
        {
            bool isValid = false;
            if (this.dbContext.Users.Any(u => u.Email == email))
            {
                isValid = true;
            }

            return isValid;
        }

        public bool IsUsernameAvailable(string username)
        {
            bool isValid = false;
            if (this.dbContext.Users.Any(u => u.Username == username))
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
