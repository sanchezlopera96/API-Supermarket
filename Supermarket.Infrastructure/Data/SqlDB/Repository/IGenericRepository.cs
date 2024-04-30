using System.Linq.Expressions;

namespace Supermarket.Infrastructure.Data.SqlDB.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<TEntity>> GetPagination(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int numberPage = 1);

        Task<TEntity> GetByID(object id);

        Task<TEntity> GetByLine(object Line);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        Task<int> CommitAsync();

        Task<IEnumerable<T>> GetAsync<T, TType>(
            Expression<Func<TEntity, bool>> where,
            Expression<Func<IGrouping<TType, TEntity>, T>> select = null,
            Expression<Func<TEntity, TType>> groupBy = null);

        Task<IEnumerable<TEntity>> GetAsync(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IQueryable<TEntity>> AsQueryable(
            Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             int? top = null);

        Task<IEnumerable<TEntity>> GetImgAsync(string id);
        void CommitTransactionAsync();

        void InsertDouble(TEntity entity);

        Task<IEnumerable<T>> ExecuteStoreProcedureAsync<T>(string procedureName, object parameters);
    }
}
