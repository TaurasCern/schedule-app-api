using Microsoft.EntityFrameworkCore;
using ScheduleAppApi.Infrastructure.Database;
using ScheduleAppApi.Infrastructure.Repositories.IRepositories;
using System.Linq.Expressions;

namespace ScheduleAppApi.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ScheduleContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ScheduleContext db)
        {
            db.Database.EnsureCreated();
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracked = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter == null)
            {
                return false;
            }

            return await query.AnyAsync(filter);
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
