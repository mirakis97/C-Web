using Andreys.Data;
using Andreys.Data.Models;
using Andreys.Data.Models.Enums;
using Andreys.Services.Contracts;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Andreys.Services.Service
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(AndreysDbContext data, IValidatorService validator) 
            : base(data, validator)
        {
        }

        public List<string> AddProduct(AddProductViewModel model)
        {
            var errrorModel = Validator.ValidateModel(model); 

            if (GetProductById(model.Id) != null)
            {
                errrorModel.Add("This Product Already Exists");
            }
            if (errrorModel.Count != 0)
            {
                return errrorModel.ToList();
            }
            ProductCategory category = (ProductCategory)Enum.Parse(typeof(ProductCategory), model.Category);
            ProductGender gender = (ProductGender)Enum.Parse(typeof(ProductGender), model.Gender);

            var newProduct = new Product
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Category = category,
                Gender = gender,
                Price = model.Price,
            };

            this.Data.Products.Add(newProduct);

            this.Data.SaveChanges();

            return errrorModel.ToList();
        }

        public ProductViewModel GetDetails(int productId)
        {
            var productById = this.GetProductById(productId);

            var album = new ProductViewModel
            {
                Id = productId,
                Name = productById.Name,
                Price = productById.Price,
                Description = productById.Description,
                ImageUrl = productById.ImageUrl,
                Category = productById.Category.ToString(),
                Gender = productById.Gender.ToString()
            };

            return album;
        }

        public IEnumerable<HomeProductsViewModel> GetProducts()
        {
            var products = this
                .GetAllProducts()
                .Select(i => new HomeProductsViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price,
                    ImageUrl = i.ImageUrl
                })
                .ToList();
            return products;
        }

        public IEnumerable<Product> GetAllProducts() => this.Data.Products.ToList();
        public Product GetProductById(int Id) => this.Data.Products.Where(x => x.Id == Id).FirstOrDefault();
    }
}
