using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Offre;

namespace Erp_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OffreController : ControllerBase
    {
        private readonly OffreManager _manager;
        private readonly IMapper _mapper;

        public OffreController(OffreManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OffreDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OffreDTO>>> GetAll()
        {
            var offres = await _manager.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<OffreDTO>>(offres));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OffreDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OffreDTO>> GetById(int id)
        {
            var offre = await _manager.GetByIdAsync(id);
            if (offre == null) return NotFound();
            return Ok(_mapper.Map<OffreDTO>(offre));
        }

        [HttpPost]
        [ProducesResponseType(typeof(OffreDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<OffreDTO>> Create([FromBody] OffreCreateDTO dto)
        {
            var offre = _mapper.Map<Offre>(dto);
            var result = await _manager.AddAsync(offre);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, _mapper.Map<OffreDTO>(result));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OffreDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OffreDTO>> Update(int id, [FromBody] OffreUpdateDTO dto)
        {
            var existing = await _manager.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _manager.UpdateAsync(existing, existing);
            return Ok(_mapper.Map<OffreDTO>(existing));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var offre = await _manager.GetByIdAsync(id);
            if (offre == null) return NotFound();

            await _manager.DeleteAsync(offre);
            return NoContent();
        }
    }
}
