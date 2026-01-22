using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Repository.Managers.Models_Managers
{
    public class FeuilleTempsManager : BaseManager<FeuilleTemps, string>, IFeuilleTempsRepository
    {
        public FeuilleTempsManager(ErpBdContext context) : base(context)
        {
        }

        public override Task<FeuilleTemps?> GetByNameAsync(string name)
        {
            return dbSet
                .Include(a => a.Employe)
                .Include(a => a.Projet)
                .FirstOrDefaultAsync(ft => ft.Employe.Nom == name || ft.Employe.Prenom == name);
        }

        public override async Task<FeuilleTemps?> GetByIdAsync(int id)
        {
            return await dbSet
                .Include(t => t.Projet)
                .Include(t => t.Employe)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<FeuilleTemps>> GetByEmployeIdAndWeekAsync(int employeId, int numsemaine, int? year = null, int? projetId = null)
        {
            int anneeCible = year ?? DateTime.UtcNow.Year;

            DateTime jan4 = new DateTime(anneeCible, 1, 4);
            int daysOffset = jan4.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)jan4.DayOfWeek - 1;
            DateTime firstMondayOfYear = jan4.AddDays(-daysOffset);

            DateTime startOfWeek = firstMondayOfYear.AddDays((numsemaine - 1) * 7);
            DateTime endOfWeek = startOfWeek.AddDays(7);

            var startOfWeekUtc = DateTime.SpecifyKind(startOfWeek, DateTimeKind.Utc);
            var endOfWeekUtc = DateTime.SpecifyKind(endOfWeek, DateTimeKind.Utc);

            var query = dbSet
                .Include(a => a.Employe)
                .Include(a => a.Projet)
                .Where(ft => ft.EmployeId == employeId)
                .Where(ft => ft.Date.Date >= startOfWeekUtc.Date && ft.Date.Date < endOfWeekUtc.Date);

            if (projetId.HasValue)
            {
                query = query.Where(ft => ft.ProjetId == projetId.Value);
            }

            return await query
                .OrderBy(ft => ft.Date)
                .ToListAsync();
        }
    }
}