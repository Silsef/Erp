using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Projet;
using System.Security.Claims; 

namespace Erp_Api.Controllers
{
    public class ProjetController : BaseController<Projet, ProjetDTO, ProjetCreateDTO, ProjetUpdateDTO, string>
    {
        public ProjetManager projetmanager;
        public ProjetController(ProjetManager manager, IMapper mapper)
            : base(manager, mapper)
        {
            projetmanager = manager;
        }

        protected override int GetEntityId(Projet entity) => entity.Id;

        public override async Task<ActionResult<ProjetDTO>> Create([FromBody] ProjetCreateDTO createDto)
        {
            var entity = _mapper.Map<Projet>(createDto);

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdString, out int userId))
            {
                entity.EmployeResponsableId = userId;
            }
            else
            {
                return Unauthorized(); 
            }
            var result = await _manager.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, _mapper.Map<ProjetDTO>(result));
        }

        [HttpGet("{entiteId}")]
        public async Task<ActionResult<IEnumerable<ProjetDTO>>> GetProjetsInternesByEntiteId(int entiteId)
        {
            var projets = await projetmanager.GetProjetsInternesByEntiteId(entiteId);

            return Ok(_mapper.Map<IEnumerable<ProjetDTO>>(projets));
        }

        [HttpGet("{entiteId}")]
        public async Task<ActionResult<IEnumerable<ProjetDTO>>> GetProjetsExternesByEntiteId(int entiteId)
        {
            var projets = await projetmanager.GetProjetsExternesByEntiteId(entiteId);

            return Ok(_mapper.Map<IEnumerable<ProjetDTO>>(projets));
        }
        [HttpGet("{employeId}")]
        public async Task<ActionResult<IEnumerable<ProjetDTO>>> GetProjetsByEmployeId(int employeId)
        {
            var projets = await projetmanager.GetProjetsByEmployeId(employeId);

            return Ok(_mapper.Map<IEnumerable<ProjetDTO>>(projets));
        }


    }
}