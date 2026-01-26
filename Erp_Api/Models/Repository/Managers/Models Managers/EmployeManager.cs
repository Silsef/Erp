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
            return await dbSet.Include(c => c.EmployeEntites).ThenInclude(c => c.Entite).Include(r=> r.Employeroles).ThenInclude(a=>a.Role).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Employe?> GetByEmailLoginAsync(string email)
        {
            return await dbSet
                .Include(c => c.Employeroles)
                    .ThenInclude(a => a.Role)
                    .Include(a=> a.Employeroles)
                .FirstOrDefaultAsync(e => e.Email == email  ||  e .Login == email);
        }

        public async Task<IEnumerable<Employe>> GetEmployeesByEntiteEtProjet(int entiteId,int? idprojet)
        {
            var query = dbSet.AsQueryable();

            query = query
                .Include(a => a.EmployeEntites)
                .Include(a => a.Employeroles)
                .ThenInclude(a => a.Role);

            if (idprojet !=null && idprojet !=0)
            {
                query = query.Where(a=>a.TachesAssignees.Any(a=>a.ProjetId == idprojet));
                query = query.Where(a=>a.ProjetsResponsable.Any(a=>a.Id == idprojet));
            }

            return await query
                .Where(e=>e.EmployeEntites.Any(ee=>ee.EntiteId == entiteId)).ToListAsync();
        }
    }
}