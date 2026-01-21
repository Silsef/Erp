using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Shared_Erp.Employe;
using Shared_Erp.Entite;

namespace Erp_Api.Controllers
{
    public class EntiteController : BaseController<Entite, EntiteDTO, EntiteCreateDTO, EntiteUpdateDTO, string>
    {
    public EntiteController(EntiteManager manager, IMapper mapper)
            : base(manager, mapper)
        {
        }

        protected override int GetEntityId(Entite entity) => entity.Id;
    }
}