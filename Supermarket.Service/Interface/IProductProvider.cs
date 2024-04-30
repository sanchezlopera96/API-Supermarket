using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;

namespace Supermarket.Service.Interface
{
    public interface IProductProvider
    {
        Task<bool> AddProduct(ProductObject productObject);
        Task<ProductObjectResult> UpdateProduct(ProductObject productObject);
        Task<bool> DeleteProduct(int productId);
        Task<IEnumerable<ProductObjectResult>> GetProducts();
        Task<ProductObjectResult> GetProduct(string productCode); 
    }
}
