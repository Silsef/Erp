using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Tache;

namespace Erp_Api.Controllers
{
    public class TacheController : BaseController<Tache, TacheDTO, TacheCreateDTO, TacheUpdateDTO, string>
    {
        public TacheManager tachemanager;
        public TacheController(TacheManager manager, IMapper mapper)
            : base(manager, mapper)
        {
            tachemanager = manager;

        }
        protected override int GetEntityId(Tache entity) => entity.Id;

        [HttpPost]
        public override async Task<ActionResult<TacheDTO>> Create([FromBody] TacheCreateDTO dto)
        {
            var entity = _mapper.Map<Tache>(dto);
            var result = await _manager.AddAsync(entity);

            var resultWithIncludes = await _manager.GetByIdAsync(result.Id);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, _mapper.Map<TacheDTO>(resultWithIncludes));
        }

        [HttpGet("{idprojet}")]
        public async Task<ActionResult<IEnumerable<TacheDTO>>> GetTachesByProjetId(int idprojet)
        {
            var taches = await tachemanager.GetByProjet(idprojet);

            return Ok(_mapper.Map<IEnumerable<TacheDTO>>(taches));
        }
    }
}
