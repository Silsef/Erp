using Shared_Erp.Entretien;

namespace Erp_Blazor.Service.Interfaces
{
    public interface IEntretienService : ICrudService<EntretienDTO, EntretienCreateDTO, EntretienUpdateDTO>
    {
        Task<List<EntretienDTO>> GetByOffreId(int offreId);
    }
}
