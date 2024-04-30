using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;
using Supermarket.Service.Services;
using System.Security.Claims;

namespace Supermarket.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IMemoryCache memoryCache)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (userRoleClaim != null && int.TryParse(userRoleClaim.Value, out int userRoleId))
            {
                if (userRoleId == 1)
                {
                    return Ok("User role: Administrator");
                }
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("AddProduct")]
        [Authorize(Roles = "1")]
        public async Task<bool> AddProduct([FromBody] ProductObject productObject)
        {
            try
            {
                return await _productService.AddProduct(productObject);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("UpdateProduct")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductObject productObject)
        {
            try
            {
                return Ok(await _productService.UpdateProduct(productObject));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteProduct")]
        [Authorize(Roles = "1")]
        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                return await _productService.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _productService.GetProducts());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetProductByCode")]
        public async Task<ProductObjectResult> GetProductByCode(string productCode)
        {
            try
            {
              var result = await _productService.GetProduct(productCode);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
