using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class OffreManager : BaseManager<Offre, string>
    {
        public OffreManager(ErpBdContext context) : base(context)
        {
        }

        public override async Task<Offre?> GetByNameAsync(string name)
        {
            return await dbSet
                .Include(o => o.TypeContrat)
                .Include(o => o.Entreprise)
                .FirstOrDefaultAsync(o => o.Titre == name);
        }

        public override async Task<IEnumerable<Offre>> GetAllAsync()
        {
            return await dbSet
                .Include(o => o.TypeContrat)
                .Include(o => o.Entreprise)
                .Include(o => o.Candidatures)
                .OrderByDescending(o => o.DatePublication)
                .ToListAsync();
        }

        public override async Task<Offre?> GetByIdAsync(int id)
        {
            return await dbSet
                .Include(o => o.TypeContrat)
                .Include(o => o.Entreprise)
                .Include(o => o.Candidatures)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
