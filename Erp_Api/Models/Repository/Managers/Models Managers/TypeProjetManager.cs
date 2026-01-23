using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using System.Data.Entity;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class TypeProjetManager : BaseManager<TypeProjet, string>, ITypeProjetRepository
    {
        public TypeProjetManager(ErpBdContext context) : base(context)
        {
        }

        public override Task<TypeProjet?> GetByNameAsync(string name)
        {
            return dbSet.FirstOrDefaultAsync(tp => tp.Nom == name);
        }
    }
}
