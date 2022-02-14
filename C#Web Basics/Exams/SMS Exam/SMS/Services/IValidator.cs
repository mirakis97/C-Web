using SMS.Models.Carts;
using SMS.Models.Products;
using SMS.Models.Users;
using System.Collections.Generic;

namespace SMS.Services
{
    public interface IValidator
    {
        (bool isValid, string errors) ValidateRegisterModel(RegisterViewFormModel model);
        (bool isValid, string errors) ValidateProductCreateModel(CreateViewModel model);
        IEnumerable<ProductListViewModel> GetProducts();
        IEnumerable<CartViewModel> GetUserProducts(string userId);
        IEnumerable<CartViewModel> AddProduct(string productId, string userId);
        void BuyProducts(string userId);
        string GetUsername(string userId);
    }
}
