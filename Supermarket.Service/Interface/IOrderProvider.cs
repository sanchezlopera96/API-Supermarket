using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;

namespace Supermarket.Service.Interface
{
    public interface IOrderProvider
    {
        Task<OrderObjectResult> AddOrder(OrderObject orderObject);
    }
}
