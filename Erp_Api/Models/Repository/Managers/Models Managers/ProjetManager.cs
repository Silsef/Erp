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
                .Include(p => p.Taches)
                .ThenInclude(d=>d.EmployeAssigne)
                .ToListAsync();
        }
    }
}
