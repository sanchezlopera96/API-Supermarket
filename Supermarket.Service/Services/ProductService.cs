using AutoMapper;
using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;
using Supermarket.Service.Interface;

namespace Supermarket.Service.Services
{
    class ProductService : IProductService
    {
        private readonly IProductProvider _provider;
        private readonly IMapper _mapper;

        public ProductService(IProductProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<bool> AddProduct(ProductObject productObject)
        {
            return await _provider.AddProduct(productObject);
        }

        public async Task<ProductObjectResult> UpdateProduct(ProductObject productObject)
        {
            return await _provider.UpdateProduct(productObject);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            return await _provider.DeleteProduct(productId);
        }

        public async Task<IEnumerable<ProductObjectResult>> GetProducts()
        {
            return await _provider.GetProducts();
        }

        public async Task<ProductObjectResult> GetProduct(string productCode)
        {
            return await _provider.GetProduct(productCode);
        }
    }
}
