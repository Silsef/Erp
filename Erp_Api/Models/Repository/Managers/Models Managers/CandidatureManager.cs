using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class CandidatureManager : BaseManager<Candidature, string>, ICandidatureRepository
    {
        public CandidatureManager(ErpBdContext context) : base(context)
        {
        }

        public override async Task<Candidature?> GetByNameAsync(string name)
        {
            return await dbSet
                .Include(c => c.Offre)
                .Include(c => c.Employe)
                .Include(c => c.Status)
                .Include(c => c.Plateforme)
                .FirstOrDefaultAsync(c => c.Nom == name);
        }

        public override async Task<IEnumerable<Candidature>> GetAllAsync()
        {
            return await dbSet
                .Include(c => c.Offre)
                .Include(c => c.Employe)
                .Include(c => c.Status)
                .Include(c => c.Plateforme)
                .OrderByDescending(c => c.DateCandidature)
                .ToListAsync();
        }

        public override async Task<Candidature?> GetByIdAsync(int id)
        {
            return await dbSet
                .Include(c => c.Offre)
                .Include(c => c.Employe)
                .Include(c => c.Status)
                .Include(c => c.Plateforme)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Candidature>> GetByOffreIdAsync(int offreId)
        {
            return await dbSet
                .Include(c => c.Offre)
                .Include(c => c.Employe)
                .Include(c => c.Status)
                .Include(c => c.Plateforme)
                .Where(c => c.OffreEmploiId == offreId)
                .OrderByDescending(c => c.DateCandidature)
                .ToListAsync();
        }
    }
}
