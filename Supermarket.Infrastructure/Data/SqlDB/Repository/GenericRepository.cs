using Microsoft.EntityFrameworkCore;
using Supermarket.Infrastructure.Data.SqlDB.Context;
using System.Linq.Expressions;
using System.Reflection;
using Supermarket.Infrastructure.Data.SqlDB.EntityFramework;

namespace Supermarket.Infrastructure.Data.SqlDB.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DBContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagination(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int numberPage = 1)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Skip(numberPage).Take(15).Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query.Skip(numberPage).Take(15)).ToListAsync();
            }
            else
            {
                return await query.Skip(numberPage).Take(15).ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetByLine(object line)
        {
            return await dbSet.FindAsync(line);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertDouble(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IQueryable<TEntity>> AsQueryable(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? top = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return top != null ? await Task.FromResult(orderBy(query).Take((int)top).AsQueryable()) : await Task.FromResult(orderBy(query).AsQueryable());
            }
            else
            {
                return top != null ? await Task.FromResult(query.Take((int)top).AsQueryable()) : await Task.FromResult(query.AsQueryable());
            }
        }

        public async Task<IEnumerable<TEntity>> GetImgAsync(string id)
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync<T, TType>(
            Expression<Func<TEntity, bool>> where,
            Expression<Func<IGrouping<TType, TEntity>, T>> select = null,
            Expression<Func<TEntity, TType>> groupBy = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (where == null)
            {
                return await query.GroupBy(groupBy).Select(select).ToListAsync();
            }
            else
            {
                IEnumerable<T> result = await query.Where(where).GroupBy(groupBy).Select(select).ToListAsync();
                return result;
            }
        }

        public Task<int> CommitAsync()
        {
            return context.SaveChangesAsync();
        }

        public void CommitTransactionAsync()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    context.ChangeTracker.Entries().ToList().ForEach(entry => entry.State = EntityState.Unchanged);
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await Task.FromResult(orderBy(query).ToList());
            }
            else
            {
                return await Task.FromResult(query.ToList());
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoreProcedureAsync<T>(string procedureName, object parameters)
        {
            var query = context.LoadStoredProc(procedureName);
            if (parameters != null)
            {
                foreach (PropertyInfo propertyInfo in parameters.GetType().GetProperties())
                {
                    query.WithSqlParam(propertyInfo.Name, propertyInfo.GetValue(parameters, null));
                }
            }

            var list = new List<T>();

            query.CommandTimeout = 300;

            await query.ExecuteStoredProcAsync((handler) =>
            {
                list = handler.ReadToList<T>().ToList();
            });

            return list;
        }
    }
}
