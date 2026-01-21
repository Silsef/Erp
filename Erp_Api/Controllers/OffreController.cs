using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Offre;
using Shared_Erp.Projet;

namespace Erp_Api.Controllers
{
    public class OffreController : BaseController<Offre, OffreDTO, OffreCreateDTO, OffreUpdateDTO, string>
    {
        public OffreController(OffreManager manager, IMapper mapper)
            : base(manager, mapper)
        {
        }

        protected override int GetEntityId(Offre entity) => entity.Id;

    }
}
