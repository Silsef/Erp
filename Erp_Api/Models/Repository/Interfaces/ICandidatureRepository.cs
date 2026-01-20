using Erp_Api.Models;
using Erp_Api.Models.Entity.Tables.Entitees;

namespace Erp_Api.Models.Repository.Interfaces
{
    public interface ICandidatureRepository
    {
        Task<IEnumerable<Candidature>> GetByOffreIdAsync(int offreId);
    }
}
