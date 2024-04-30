using AutoMapper;
using Supermarket.Domain.Model;
using Supermarket.Domain.ObjectResult;
using Supermarket.Service.Interface;

namespace Supermarket.Service.Services
{
    class OrderService : IOrderService
    {
        private readonly IOrderProvider _provider;
        private readonly IMapper _mapper;

        public OrderService(IOrderProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<OrderObjectResult> AddOrder(OrderObject orderObject)
        {
            return await _provider.AddOrder(orderObject);
        }
    }
}