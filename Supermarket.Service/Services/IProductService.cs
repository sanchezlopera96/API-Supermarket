using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;

namespace Supermarket.Service.Services
{
    public interface IProductService
    {
        Task<bool> AddProduct(ProductObject productObject);
        Task<ProductObjectResult> UpdateProduct(ProductObject productObject);
        Task<bool> DeleteProduct(int productId);
        Task<IEnumerable<ProductObjectResult>> GetProducts();
        Task<ProductObjectResult> GetProduct(string productCode);
    }
}
