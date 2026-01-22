using Shared_Erp.FeuilleTemps;

namespace Erp_Blazor.Service.Interfaces
{
    public interface IFeuilleTempsService : ICrudService<FeuilleTempsDTO, FeuilleTempsCreateDTO, FeuilleTempsUpdateDTO>
    {
        Task<List<FeuilleTempsDTO>> GetBySemaine(int employeId, int numSemaine, int? annee = null, int? projetId = null);
    }
}
