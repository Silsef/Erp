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
        public ProjetController(ProjetManager manager, IMapper mapper)
            : base(manager, mapper)
        {
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
    }
}