using Andreys.ViewModels.Products;
using System.Collections.Generic;

namespace Andreys.Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<HomeProductsViewModel> GetProducts();
        ProductViewModel GetDetails(int productId);
        List<string> AddProduct(AddProductViewModel model);
    }
}
