using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class ProjetManager : BaseManager<Projet, string>, IProjetRepository
    {
        public ProjetManager(ErpBdContext context) : base(context)
        {
        }

        public override async Task<Projet?> GetByNameAsync(string name)
        {
            return await dbSet.FirstOrDefaultAsync(a => a.Nom == name);
        }
        public override async Task<IEnumerable<Projet>> GetAllAsync()
        {
            return await dbSet
                .Include(p => p.EntiteRealisatrice)
                .Include(p => p.EntiteCliente)
                .Include(p => p.EmployeResponsable)
               .Include(d => d.TypeProjet)
                .Include(p => p.Taches)
                .ThenInclude(d=>d.EmployeAssigne)
                .ToListAsync();
        }

        public async Task<IEnumerable<Projet>> GetProjetsInternesByEntiteId(int entiteId)
        {
            return await dbSet
               .Include(p => p.EntiteRealisatrice)
               .Include(p => p.EntiteCliente)
               .Include(p => p.EmployeResponsable)
               .Include(d => d.TypeProjet)
               .Include(p => p.Taches)
               .ThenInclude(d => d.EmployeAssigne)
               .Where(d=> d.EntiteClienteId == entiteId && d.EntiteRealisatriceId == entiteId)
               .ToListAsync();
        }

        public async Task<IEnumerable<Projet>> GetProjetsExternesByEntiteId(int entiteId)
        {
            return await dbSet
               .Include(p => p.EntiteRealisatrice)
               .Include(p => p.EntiteCliente)
               .Include(p => p.EmployeResponsable)
               .Include(p => p.Taches)
               .ThenInclude(d => d.EmployeAssigne)
               .Include(d=> d.TypeProjet)
               .Where(d => d.EntiteRealisatriceId == entiteId && d.EntiteClienteId !=entiteId)
               .ToListAsync();
        }
        public async Task<IEnumerable<Projet>> GetProjetsByEmployeId(int employeid)
        {
            return await dbSet
              .Include(p => p.EntiteRealisatrice)
              .Include(p => p.EntiteCliente)
              .Include(p => p.EmployeResponsable)
              .Include(p => p.Taches)
              .ThenInclude(d => d.EmployeAssigne)
              .Include(d => d.TypeProjet)
              .Where(d => d.Taches.Any(d => d.EmployeAssigneId == employeid) || d.EmployeResponsableId == employeid)
              .ToListAsync();
        }
    }
}
