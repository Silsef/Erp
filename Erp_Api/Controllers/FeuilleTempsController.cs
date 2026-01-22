using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.FeuilleTemps;
using Shared_Erp.Projet;
using System.Security.Claims;

namespace Erp_Api.Controllers
{
    public class FeuilleTempsController : BaseController<FeuilleTemps, FeuilleTempsDTO, FeuilleTempsCreateDTO, FeuilleTempsUpdateDTO, string>
    {
        private readonly FeuilleTempsManager _feuilleTempsManager;

        public FeuilleTempsController(FeuilleTempsManager manager, IMapper mapper) : base(manager, mapper)
        {
            _feuilleTempsManager = manager;
        }

        protected override int GetEntityId(FeuilleTemps entity) => entity.Id;

        [HttpGet("GetBySemaine/{employeId}/{numSemaine}")]
        public async Task<ActionResult<IEnumerable<FeuilleTempsDTO>>> GetBySemaine(int employeId, int numSemaine, [FromQuery] int? annee = null)
        {
            var result = await _feuilleTempsManager.GetByEmployeIdAndWeekAsync(employeId, numSemaine, annee);

            return Ok(_mapper.Map<IEnumerable<FeuilleTempsDTO>>(result));
        }


        public override async Task<ActionResult<FeuilleTempsDTO>> Create([FromBody] FeuilleTempsCreateDTO createDto)
        {
            var entity = _mapper.Map<FeuilleTemps>(createDto);

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdString, out int userId))
            {
                entity.EmployeId = userId;
            }
            else
            {
                return Unauthorized();
            }
            var result = await _manager.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, _mapper.Map<FeuilleTempsDTO>(result));
        }
    }
}