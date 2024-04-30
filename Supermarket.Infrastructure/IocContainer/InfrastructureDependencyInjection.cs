using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Supermarket.Infrastructure.Data.SqlDB.Context;
using Supermarket.Infrastructure.Data.SqlDB.Provider;
using Supermarket.Infrastructure.Data.SqlDB.Repository;
using Supermarket.Infrastructure.Data.SqlDB.UnitOfWork;
using Supermarket.Service.Interface;

namespace Supermarket.Infrastructure.IocContainer
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IProductProvider, ProductProvider>();
            services.AddScoped<IOrderProvider, OrderProvider>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddDbContext<DBContext>(options =>
            {
                var connection = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connection);
            });

            services.AddControllers().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }
    }
}
