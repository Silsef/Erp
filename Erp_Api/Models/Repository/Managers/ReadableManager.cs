using Erp_Api.Models.Entity;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Erp_Api.Models.Repository.Managers
{
    public abstract class ReadableManager<TEntity> : ReadableRepository<TEntity> where TEntity : class
    {
        protected readonly ErpBdContext context;
        protected readonly DbSet<TEntity> dbSet;

        public ReadableManager(ErpBdContext context)
        {
            this.context = context;
            this.dbSet = context?.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
