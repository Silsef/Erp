using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class EntretienManager : BaseManager<Entretien, string>, IEntretienRepository
    {
        public EntretienManager(ErpBdContext context) : base(context)
        {
        }


        public override async Task<IEnumerable<Entretien>> GetAllAsync()
        {
            return await dbSet
                .Include(e => e.Candidature)
                    .ThenInclude(c => c.Offre)
                .Include(e => e.Interviewer)
                .Include(e => e.TypeEntretien)
                .OrderByDescending(e => e.DateEntretien)
                .ToListAsync();
        }

        public override async Task<Entretien?> GetByIdAsync(int id)
        {
            return await dbSet
                .Include(e => e.Candidature)
                    .ThenInclude(c => c.Offre)
                .Include(e => e.Interviewer)
                .Include(e => e.TypeEntretien)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Entretien>> GetByCandidatureIdAsync(int candidatureId)
        {
            return await dbSet
                .Include(e => e.Candidature)
                    .ThenInclude(c => c.Offre)
                .Include(e => e.Interviewer)
                .Include(e => e.TypeEntretien)
                .Where(e => e.CandidatureId == candidatureId)
                .OrderByDescending(e => e.DateEntretien)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entretien>> GetByOffreIdAsync(int offreId)
        {
            return await dbSet
                .Include(e => e.Candidature)
                    .ThenInclude(c => c.Offre)
                .Include(e => e.Interviewer)
                .Include(e => e.TypeEntretien)
                .Where(e => e.Candidature.OffreEmploiId == offreId)
                .OrderByDescending(e => e.DateEntretien)
                .ToListAsync();
        }

        public override Task<Entretien?> GetByNameAsync(string name)
        {
            return null;
        }
    }
}
