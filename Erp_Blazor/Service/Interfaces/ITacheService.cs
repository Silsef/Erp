using Shared_Erp.Tache;

namespace Erp_Blazor.Service.Interfaces
{
    public interface ITacheService : ICrudService<TacheDTO, TacheCreateDTO, TacheUpdateDTO>
    {
        Task<List<TacheDTO>> GetTachesByProjetId(int idprojet);
    }
}
