using Shared_Erp.Candidature;

namespace Erp_Blazor.Service.Interfaces
{
    public interface ICandidatureService : ICrudService<CandidatureDTO, CandidatureCreateDTO, CandidatureUpdateDTO>
    {
        Task<List<CandidatureDTO>> GetByOffreId(int offreId);
    }
}
