using Erp_Api.Models.Entity.Tables.Entitees;

namespace Erp_Api.Models.Repository.Interfaces
{
    public interface IProjetRepository
    {
        Task<IEnumerable<Projet>> GetProjetsInternesByEntiteId(int entiteId);
        Task<IEnumerable<Projet>> GetProjetsExternesByEntiteId(int entiteId);
    }
}
