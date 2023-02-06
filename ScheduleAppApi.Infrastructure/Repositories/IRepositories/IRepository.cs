using System.Linq.Expressions;

namespace ScheduleAppApi.Infrastructure.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {           
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracked = true);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> CreateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task SaveAsync();
    }
}
