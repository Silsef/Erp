using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class EntiteManager : BaseManager<Entite, string>, IEntiteRepository
    {
        public EntiteManager(ErpBdContext context) : base(context)
        {
        }

        public override async Task<Entite?> GetByNameAsync(string name)
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Nom == name);
        }

        public async Task<IEnumerable<Entite>> GetEntitesByEmployeId(int employeid)
        {
            return await dbSet
                .Include(e => e.EmployeEntites)
                .Where(e=>e.EmployeEntites.Any(ee=>ee.EmployeId == employeid))
                .ToListAsync();
        }
    }
}
