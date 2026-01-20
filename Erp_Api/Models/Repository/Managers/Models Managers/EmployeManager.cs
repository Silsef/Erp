using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class EmployeManager : BaseManager<Employe, string> , IEmployeRepository
    {
        public EmployeManager(ErpBdContext context) : base(context)
        {
        }

        public override async Task<Employe?> GetByNameAsync(string name)
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Nom == name);
        }

        public override async Task<Employe?> GetByIdAsync(int id)
        {
            return await dbSet.Include(c => c.EmployeEntreprises).ThenInclude(c => c.Entreprise).Include(r=> r.Employeroles).ThenInclude(a=>a.Role).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Employe?> GetByEmailAsync(string email)
        {
            return await dbSet
                .Include(c => c.Employeroles)
                    .ThenInclude(a => a.Role)
                .FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
