using Erp_Api.Models.Entity.Tables.Entitees;

namespace Erp_Api.Models.Repository.Interfaces
{
    public interface IEmployeRepository
    {
        Task<Employe?> GetByEmailAsync(string email);
    }
}
