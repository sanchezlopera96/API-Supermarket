using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;
using Supermarket.Service.Services;

namespace Supermarket.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, IMemoryCache memoryCache)
        {
            _orderService = orderService;
        }


        [HttpPost]
        [Route("AddOrder")]

        public async Task<IActionResult> AddOrder([FromBody] OrderObject orderObject)
        {
            try
            {
                return Ok(await _orderService.AddOrder(orderObject));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}