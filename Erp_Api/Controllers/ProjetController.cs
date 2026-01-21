// Erp_Api/Controllers/ProjetController.cs
using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Projet;

namespace Erp_Api.Controllers
{
    public class ProjetController : BaseController<Projet, ProjetDTO, ProjetCreateDTO, ProjetUpdateDTO, string>
    {
        public ProjetController(ProjetManager manager, IMapper mapper)
            : base(manager, mapper)
        {
        }

        protected override int GetEntityId(Projet entity) => entity.Id;

    }
}