using Supermarket.Domain.ObjectResult;
using Supermarket.Infrastructure.Data.SqlDB.Entities;
using Supermarket.Infrastructure.Data.SqlDB.Repository;

namespace Supermarket.Infrastructure.Data.SqlDB.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<ProductObjectEntity> ProductObjectRepository { get; }
        IGenericRepository<ProductObjectResultEntity> ProductResultRepository { get; }
        IGenericRepository<OrderHeaderObjectEntity> OrderHeaderObjectRepository { get; }
        IGenericRepository<OrderDetailObjectEntity> OrderDetailObjectRepository { get; }
        IGenericRepository<OrderObjectResultEntity> OrderObjectResultRepository { get; }
        void Save();
    }
}
