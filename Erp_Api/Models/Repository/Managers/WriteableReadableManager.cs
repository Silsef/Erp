using Erp_Api.Models.Entity;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers
{
    public class WriteableReadableManager<TEntity> : WritableRepository<TEntity>, ReadableRepository<TEntity> where TEntity : class
    {

        protected readonly ErpBdContext context;
        protected readonly DbSet<TEntity> dbSet;

        public WriteableReadableManager(ErpBdContext context)
        {
            this.context = context;
            this.dbSet = context?.Set<TEntity>();
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task UpdateAsync(TEntity entityToUpdate, TEntity entity)
        {
            context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }
    }
}
