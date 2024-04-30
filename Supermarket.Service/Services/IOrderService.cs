using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;

namespace Supermarket.Service.Services
{
    public interface IOrderService
    {
        Task<OrderObjectResult> AddOrder(OrderObject orderObject);
    }
}
