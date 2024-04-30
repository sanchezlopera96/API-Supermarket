using Microsoft.Extensions.DependencyInjection;
using Supermarket.Service.Services;

namespace Supermarket.Service.IocContainer
{
    public static class ServicesDependencyInjection
    {
        public static void AddServicesDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
