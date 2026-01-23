using Erp_Api.Models.Entity.Tables.Entitees;

namespace Erp_Api.Models.Repository.Interfaces
{
    public interface ITacheRepository 
    {
        Task<IEnumerable<Tache>> GetByProjet(int idproj);
    }
}
