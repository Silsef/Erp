using Erp_Api.Models.Entity;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers
{
    public abstract class WritableManager<TEntity> : WritableRepository<TEntity>
    where TEntity : class
    {
        protected readonly ErpBdContext _context;
        protected readonly DbSet<TEntity> dbSet;

        protected WritableManager(ErpBdContext context)
        {
            _context = context;
            dbSet = context?.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entityToUpdate, TEntity entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
