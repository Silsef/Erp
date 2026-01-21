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
    }
}
