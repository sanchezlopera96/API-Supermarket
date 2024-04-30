using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Supermarket.Domain.ObjectResult;
using Supermarket.Infrastructure.Data.SqlDB.Entities;

namespace Supermarket.Infrastructure.Data.SqlDB.Context
{
    public class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    .Build();

                string cnnString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(cnnString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductObjectEntity>().ToTable("Productos").HasKey(p => p.IdProducto);
            modelBuilder.Entity<OrderDetailObjectEntity>().ToTable("PedidoDetalle").HasKey(p => p.Id);
            modelBuilder.Entity<OrderHeaderObjectEntity>().ToTable("PedidoEncabezado").HasKey(p => p.Id);
        }
    }
}
