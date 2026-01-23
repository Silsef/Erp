using Erp_Api.Models.Entity.Tables.Entitees;

namespace Erp_Api.Models.Repository.Interfaces
{
    public interface IEntiteRepository
    {
        Task<IEnumerable<Entite>> GetEntitesByEmployeId(int employeid);
    }
}
