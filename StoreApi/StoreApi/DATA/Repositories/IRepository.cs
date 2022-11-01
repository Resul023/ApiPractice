using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using WebApplication1.Entities;

namespace StoreApi.DATA.Repositories
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression);
        Task<int> CommitAsync();
        int Commit();
        void Remove(TEntity entity);
    }
}
