using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Employe;
using Shared_Erp.Entite;

namespace Erp_Api.Controllers
{
    public class EntiteController : BaseController<Entite, EntiteDTO, EntiteCreateDTO, EntiteUpdateDTO, string>
    {
        public EntiteManager entiteManager;
        public EntiteController(EntiteManager manager, IMapper mapper)
            : base(manager, mapper)
        {
            entiteManager = manager;
        }

        protected override int GetEntityId(Entite entity) => entity.Id;

        [HttpGet("{idemploye}")]
        public async Task<ActionResult<IEnumerable<EntiteDTO>>> GetEntiteByIdEmploye(int idemploye)
        {
            var entite = await entiteManager.GetEntitesByEmployeId(idemploye);
            if (entite == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EntiteDTO>>(entite));
        }
    }
}