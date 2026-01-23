using Shared_Erp.Entite;

namespace Erp_Blazor.Service.Interfaces
{
    public interface IEntiteService : ICrudService<EntiteDTO, EntiteCreateDTO, EntiteUpdateDTO>
    {
        Task<List<EntiteDTO>> GetByIdEmploye(int idemp);
    }
}
