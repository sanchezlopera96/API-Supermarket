using Supermarket.Domain.ObjectResult;
using Supermarket.Infrastructure.Data.SqlDB.Context;
using Supermarket.Infrastructure.Data.SqlDB.Entities;
using Supermarket.Infrastructure.Data.SqlDB.Repository;

namespace Supermarket.Infrastructure.Data.SqlDB.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DBContext Context;

        public IGenericRepository<ProductObjectEntity> ProductObjectRepository { get; }
        public IGenericRepository<ProductObjectResultEntity> ProductResultRepository { get; }
        public IGenericRepository<OrderHeaderObjectEntity> OrderHeaderObjectRepository { get; }
        public IGenericRepository<OrderDetailObjectEntity> OrderDetailObjectRepository { get; }
        public IGenericRepository<OrderObjectResultEntity> OrderObjectResultRepository { get; }

        public UnitOfWork(DBContext context,
            IGenericRepository<ProductObjectEntity> productObjectRepository,
            IGenericRepository<ProductObjectResultEntity> productResultRepository,
            IGenericRepository<OrderHeaderObjectEntity> orderHeaderObjectRepository,
            IGenericRepository<OrderDetailObjectEntity> orderDetailObjectRepository,
            IGenericRepository<OrderObjectResultEntity> orderObjectResultRepository
            )
        {
            this.Context = context;
            ProductResultRepository = productResultRepository;
            ProductObjectRepository = productObjectRepository;
            OrderHeaderObjectRepository = orderHeaderObjectRepository;
            OrderDetailObjectRepository = orderDetailObjectRepository;
            OrderObjectResultRepository = orderObjectResultRepository;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
