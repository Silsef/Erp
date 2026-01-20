using Erp_Api.Models;
using Erp_Api.Models.Entity.Tables.Entitees;

namespace Erp_Api.Models.Repository.Interfaces
{
    public interface IEntretienRepository 
    {
        Task<IEnumerable<Entretien>> GetByCandidatureIdAsync(int candidatureId);
        Task<IEnumerable<Entretien>> GetByOffreIdAsync(int offreId);
    }
}
