using Erp_Api.Models.Entity.Tables.Entitees;

namespace Erp_Api.Models.Repository.Interfaces
{
    public interface IFeuilleTempsRepository
    {
        Task<IEnumerable<FeuilleTemps>> GetByEmployeIdAndWeekAsync(int employeId, int numsemaine, int? year = null, int? projetId = null);
    }
}
