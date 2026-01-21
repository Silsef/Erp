// Erp_Api/Controllers/BaseController.cs
using AutoMapper;
using Erp_Api.Models.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erp_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public abstract class BaseController<TEntity, TDto, TCreateDto, TUpdateDto, TKey> : ControllerBase
        where TEntity : class
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly IDataRepository<TEntity, TKey> _manager;
        protected readonly IMapper _mapper;

        protected BaseController(IDataRepository<TEntity, TKey> manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
        {
            var entities = await _manager.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TDto>>(entities));
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDto>> GetById(int id)
        {
            var entity = await _manager.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<TDto>(entity));
        }

        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var result = await _manager.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = GetEntityId(result) }, _mapper.Map<TDto>(result));
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TDto>> Update(int id, [FromBody] TUpdateDto dto)
        {
            var existing = await _manager.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _manager.UpdateAsync(existing, existing);
            return Ok(_mapper.Map<TDto>(existing));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _manager.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _manager.DeleteAsync(entity);
            return NoContent();
        }

        protected abstract int GetEntityId(TEntity entity);
    }
}