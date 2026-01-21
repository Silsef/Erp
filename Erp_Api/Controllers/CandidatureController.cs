using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Candidature;
using Shared_Erp.Projet;

namespace Erp_Api.Controllers
{
    public class CandidatureController : BaseController<Candidature, CandidatureDTO, CandidatureCreateDTO, CandidatureUpdateDTO, string>
    {
        public CandidatureController(CandidatureManager manager, IMapper mapper)
            : base(manager, mapper)
        {
        }

        protected override int GetEntityId(Candidature entity) => entity.Id;

    }
}
