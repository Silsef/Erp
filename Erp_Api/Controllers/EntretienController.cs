using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Entretien;
using Shared_Erp.Projet;

namespace Erp_Api.Controllers
{
    public class EntretienController : BaseController<Entretien, EntretienDTO, EntretienCreateDTO, EntretienUpdateDTO, string>
    {
        public EntretienController(EntretienManager manager, IMapper mapper)
            : base(manager, mapper)
        {
        }

        protected override int GetEntityId(Entretien entity) => entity.Id;

    }
}
