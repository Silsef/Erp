using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Shared_Erp.TypeProjet;

namespace Erp_Api.Controllers
{
    public class TypeProjetController : BaseController<TypeProjet, TypeProjetDTO, TypeProjetCreateDTO, TypeProjetUpdateDTO, string>
    {
        TypeProjetManager typeprojetmanager;
        public TypeProjetController(TypeProjetManager manager, IMapper mapper) : base(manager, mapper)
        {
            typeprojetmanager = manager;
        }

        protected override int GetEntityId(TypeProjet entity) => entity.Id;


    }
}
