using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Models.Carts;
using SMS.Models.Products;
using SMS.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Services
{
    public class Validator : IValidator
    {
        private readonly SMSDbContext dbContext;

        public Validator(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<CartViewModel> AddProduct(string productId, string userId)
        {
            var user = dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefault();

            var product = dbContext.Products.FirstOrDefault(p => p.Id == productId);

            user.Cart.Products.Add(product);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
            return user
                .Cart
                .Products
                .Select(p => new CartViewModel()
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price.ToString("F2")
                });
        }

        public void BuyProducts(string userId)
        {
            var user = dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefault();

            user.Cart.Products.Clear();

            dbContext.SaveChanges();
        }

        public IEnumerable<ProductListViewModel> GetProducts()
        {
            return dbContext.Products
                .Select(t => new ProductListViewModel
                {
                    ProductName = t.Name,
                    ProductPrice = t.Price.ToString("F2"),
                    ProductId = t.Id
                }).ToList();
        }

        public string GetUsername(string userId)
        {
            return dbContext.Users.FirstOrDefault(u => u.Id == userId)?.Username;
        }

        public IEnumerable<CartViewModel> GetUserProducts(string userId)
        {
            var user = dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefault();

            return user
                .Cart
                .Products
                .Select(p => new CartViewModel()
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price.ToString("F2")
                });
        }

        public (bool isValid, string errors) ValidateProductCreateModel(CreateViewModel model)
        {
            bool isValid = true;
            StringBuilder errors = new StringBuilder();

            if (model == null)
            {
                return (false, "Create model is required!");
            }

            if (model.Name == null || model.Name.Length < 4 || model.Name.Length > 20)
            {
                isValid = false;
                errors.AppendLine("Product name must be between 4 and 20 long and is required!");
            }
            if (model.Price < 0.05m || model.Price > 1000m)
            {
                isValid = false;
                errors.AppendLine("Price must be between 0.05 and 1000");
            }

            return (isValid, errors.ToString());
        }
        public (bool isValid, string errors) ValidateRegisterModel(RegisterViewFormModel model)
        {
            bool isValid = true;
            StringBuilder errors = new StringBuilder();

            if (model == null)
            {
                return (false, "Register model is required!");
            }

            if (model.Username == null || model.Username.Length < 5 || model.Username.Length > 20)
            {
                isValid = false;
                errors.AppendLine("Username must be between 5 and 20 elements long and is required!");
            }
            if (model.Password == null || model.Password.Length < 6 || model.Password.Length > 20)
            {
                isValid = false;
                errors.AppendLine("Password length must be between 6 and 20 sybmols!");
            }
            if (model.Password != model.ConfirmPassword)
            {
                isValid = false;
                errors.AppendLine("Passwords and its confimation do not match!");
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                isValid = false;
                errors.AppendLine("Email is required!");
            }
            if (this.dbContext.Users.Any(u => u.Username == model.Username))
            {
                isValid = false;
                errors.AppendLine($"User with '{model.Username}' username already exist!");
            }
            if (this.dbContext.Users.Any(u => u.Email == model.Email))
            {
                isValid = false;
                errors.AppendLine($"User with '{model.Email}' e-mail already exist!");
            }
            return (isValid, errors.ToString());
        }
    }
}
