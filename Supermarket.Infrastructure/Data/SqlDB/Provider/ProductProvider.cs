using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;
using Supermarket.Infrastructure.Data.SqlDB.Entities;
using Supermarket.Infrastructure.Data.SqlDB.UnitOfWork;
using Supermarket.Service.Interface;

namespace Supermarket.Infrastructure.Data.SqlDB.Provider
{
    public class ProductProvider : IProductProvider
    {
        public readonly IUnitOfWork _uowSupermarket;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ProductProvider(IUnitOfWork uowSupermarket, IMapper mapper, IMemoryCache memoryCache)
        {
            _uowSupermarket = uowSupermarket;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<bool> AddProduct(ProductObject productObject)
        {
            IEnumerable<ProductObjectEntity> lproducts = await _uowSupermarket.ProductObjectRepository.GetAsync(p => p.CodigoProducto == productObject.ProductCode);

            if (!lproducts.Any())
            {
                ProductObjectEntity add;
                add = _mapper.Map<ProductObjectEntity>(productObject);
                _uowSupermarket.ProductObjectRepository.Insert(add);
                await _uowSupermarket.ProductObjectRepository.CommitAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ProductObjectResult> UpdateProduct(ProductObject productObject)
        {
            try
            {
                ProductObjectEntity update;
                update = _mapper.Map<ProductObjectEntity>(productObject);
                _uowSupermarket.ProductObjectRepository.Update(update);
                await _uowSupermarket.ProductObjectRepository.CommitAsync();
                object parameters = new
                {
                    @CodigoProducto = productObject.ProductCode
                };
                IEnumerable<ProductObjectResultEntity> products = await _uowSupermarket.ProductResultRepository.ExecuteStoreProcedureAsync<ProductObjectResultEntity>("SP_ConsultarInformacionProductosPorCodigo", parameters);
                ProductObjectResultEntity result = products.FirstOrDefault();
                return _mapper.Map<ProductObjectResult>(result);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating product data: {ex.Message}");
            }

        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var lproducts = await _uowSupermarket.ProductObjectRepository.GetAsync(p => p.IdProducto == productId);

            if (lproducts != null)
            {
                _uowSupermarket.ProductObjectRepository.Delete(lproducts.FirstOrDefault());
                await _uowSupermarket.ProductObjectRepository.CommitAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductObjectResult>> GetProducts()
        {
            try
            {

                object parameters = new
                {
                    @Estado = true
                };
                IEnumerable<ProductObjectResultEntity> products = await _uowSupermarket.ProductResultRepository.ExecuteStoreProcedureAsync<ProductObjectResultEntity>("SP_ConsultarInformacionProductos", parameters);
                return _mapper.Map<IEnumerable<ProductObjectResult>>(products);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving product data: {ex.Message}");
            }
        }

        public async Task<ProductObjectResult> GetProduct(string productCode)
        {
            try
            {
                object parameters = new
                {
                    @Estado = true
                };

                IEnumerable<ProductObjectResultEntity> products = await _uowSupermarket.ProductResultRepository.ExecuteStoreProcedureAsync<ProductObjectResultEntity>("SP_ConsultarInformacionProductos", parameters);

                var product = _mapper.Map<IEnumerable<ProductObjectResult>>(products)
                                  .FirstOrDefault(p => p.ProductCode == productCode);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving product data: {ex.Message}");
            }
        }
    }
}