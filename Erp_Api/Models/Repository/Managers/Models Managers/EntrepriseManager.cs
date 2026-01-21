using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class EntrepriseManager : BaseManager<Entreprise, string>, IEntrepriseRepository
    {
        public EntrepriseManager(ErpBdContext context) : base(context)
        {
        }

        public override async Task<Entreprise?> GetByNameAsync(string name)
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Nom == name);
        }
    }
}
