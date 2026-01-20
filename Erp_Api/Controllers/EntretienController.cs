using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Entretien;

namespace Erp_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EntretienController : ControllerBase
    {
        private readonly EntretienManager _manager;
        private readonly IMapper _mapper;

        public EntretienController(EntretienManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EntretienDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EntretienDTO>>> GetAll()
        {
            var entretiens = await _manager.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<EntretienDTO>>(entretiens));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EntretienDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EntretienDTO>> GetById(int id)
        {
            var entretien = await _manager.GetByIdAsync(id);
            if (entretien == null) return NotFound();
            return Ok(_mapper.Map<EntretienDTO>(entretien));
        }

        [HttpGet("{offreId}")]
        [ProducesResponseType(typeof(IEnumerable<EntretienDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EntretienDTO>>> GetByOffreId(int offreId)
        {
            var entretiens = await _manager.GetByOffreIdAsync(offreId);
            return Ok(_mapper.Map<IEnumerable<EntretienDTO>>(entretiens));
        }

        [HttpPost]
        [ProducesResponseType(typeof(EntretienDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<EntretienDTO>> Create([FromBody] EntretienCreateDTO dto)
        {
            var entretien = _mapper.Map<Entretien>(dto);
            var result = await _manager.AddAsync(entretien);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, _mapper.Map<EntretienDTO>(result));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EntretienDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EntretienDTO>> Update(int id, [FromBody] EntretienUpdateDTO dto)
        {
            var existing = await _manager.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _manager.UpdateAsync(existing, existing);
            return Ok(_mapper.Map<EntretienDTO>(existing));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entretien = await _manager.GetByIdAsync(id);
            if (entretien == null) return NotFound();

            await _manager.DeleteAsync(entretien);
            return NoContent();
        }
    }
}
