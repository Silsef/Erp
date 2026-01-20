using Shared_Erp.Employe;

namespace Erp_Blazor.Service.Interfaces
{
    public interface IEmployeService : ICrudService<EmployeDTO, EmployeCreateDTO, EmployeUpdateDTO>
    {
        Task<EmployeDTO> GetByNom(string nom);
    }
}
