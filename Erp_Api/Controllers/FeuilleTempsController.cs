using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.FeuilleTemps;
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

        /// <summary>
        /// Récupère les feuilles de temps d'une semaine
        /// Si employeId n'est pas fourni, utilise l'utilisateur connecté
        /// </summary>
        [HttpGet("{numSemaine}")]
        [HttpGet("{employeId}/{numSemaine}")]
        public async Task<ActionResult<IEnumerable<FeuilleTempsDTO>>> GetBySemaine(
        int numSemaine,
        int? employeId = null,
        [FromQuery] int? annee = null,
        [FromQuery] int? projetId = null) 
        {
            int effectiveEmployeId;

            if (employeId.HasValue)
            {
                effectiveEmployeId = employeId.Value;
            }
            else
            {
                var userIdClaim = User.FindFirst("id")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out effectiveEmployeId))
                {
                    return Unauthorized("Utilisateur non authentifié");
                }
            }

            var result = await _feuilleTempsManager.GetByEmployeIdAndWeekAsync(effectiveEmployeId, numSemaine, annee, projetId);
            return Ok(_mapper.Map<IEnumerable<FeuilleTempsDTO>>(result));
        }

        public override async Task<ActionResult<FeuilleTempsDTO>> Create([FromBody] FeuilleTempsCreateDTO createDto)
        {
            var entity = _mapper.Map<FeuilleTemps>(createDto);

            var userIdString = User.FindFirst("id")?.Value;

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