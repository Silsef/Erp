using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Candidature;

namespace Erp_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CandidatureController : ControllerBase
    {
        private readonly CandidatureManager _manager;
        private readonly IMapper _mapper;

        public CandidatureController(CandidatureManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CandidatureDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CandidatureDTO>>> GetAll()
        {
            var candidatures = await _manager.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CandidatureDTO>>(candidatures));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CandidatureDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CandidatureDTO>> GetById(int id)
        {
            var candidature = await _manager.GetByIdAsync(id);
            if (candidature == null) return NotFound();
            return Ok(_mapper.Map<CandidatureDTO>(candidature));
        }

        [HttpGet("{offreId}")]
        [ProducesResponseType(typeof(IEnumerable<CandidatureDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CandidatureDTO>>> GetByOffreId(int offreId)
        {
            var candidatures = await _manager.GetByOffreIdAsync(offreId);
            return Ok(_mapper.Map<IEnumerable<CandidatureDTO>>(candidatures));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CandidatureDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<CandidatureDTO>> Create([FromBody] CandidatureCreateDTO dto)
        {
            var candidature = _mapper.Map<Candidature>(dto);
            var result = await _manager.AddAsync(candidature);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, _mapper.Map<CandidatureDTO>(result));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CandidatureDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CandidatureDTO>> Update(int id, [FromBody] CandidatureUpdateDTO dto)
        {
            var existing = await _manager.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _manager.UpdateAsync(existing, existing);
            return Ok(_mapper.Map<CandidatureDTO>(existing));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var candidature = await _manager.GetByIdAsync(id);
            if (candidature == null) return NotFound();

            await _manager.DeleteAsync(candidature);
            return NoContent();
        }
    }
}
