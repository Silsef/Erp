using Shared_Erp.Projet;

namespace Erp_Blazor.Service.Interfaces
{
    public interface IProjetService : ICrudService<ProjetDTO, ProjetCreateDTO, ProjetUpdateDTO>
    {
        Task<List<ProjetDTO>> GetProjetsExternesByEntiteId(int entiteId);
        Task<List<ProjetDTO>> GetProjetsInternesByEntiteId(int entiteId);
    }
}
