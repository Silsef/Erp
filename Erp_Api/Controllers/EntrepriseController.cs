using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Shared_Erp.Employe;
using Shared_Erp.Entreprise;

namespace Erp_Api.Controllers
{
    public class EntrepriseController : BaseController<Entreprise, EntrepriseDTO, EntrepriseCreateDTO, EntrepriseUpdateDTO, string>
    {
    public EntrepriseController(EntrepriseManager manager, IMapper mapper)
            : base(manager, mapper)
        {
        }

        protected override int GetEntityId(Entreprise entity) => entity.Id;
    }
}