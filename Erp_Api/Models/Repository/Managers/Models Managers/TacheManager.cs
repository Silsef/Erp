using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class TacheManager : BaseManager<Tache, string>, ITacheRepository
    {
        public TacheManager(ErpBdContext context) : base(context)
        {
        }

        public override async Task<Tache?> GetByNameAsync(string name)
        {
               return await dbSet.FirstOrDefaultAsync(t => t.Nom == name);
        }

        public override async Task<IEnumerable<Tache>> GetAllAsync()
        {
            return await dbSet
                .Include(t=>t.EmployeAssigne)
                .Include(t=>t.Projet)
                .Include(t=>t.TacheParente)
                .Include(t=>t.SousTaches)
                .ToListAsync();
        }
        public override async Task<Tache?> GetByIdAsync(int id)
        {
            return await dbSet
                .Include(t => t.EmployeAssigne)
                .Include(t => t.Projet)
                .Include(t => t.TacheParente)
                .Include(t => t.SousTaches)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tache>> GetByProjet(int idproj)
        {
            return await dbSet
                .Include(t => t.EmployeAssigne)
                .Include(t => t.Projet)
                .Include(t => t.TacheParente)
                .Include(t => t.SousTaches)
                .Where(t => t.ProjetId == idproj).ToListAsync();
        }
    }
}
