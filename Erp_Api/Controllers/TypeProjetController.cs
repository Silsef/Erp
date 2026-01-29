using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Tache;
using Shared_Erp.TypeProjet;

namespace Erp_Api.Controllers
{
    public class TypeProjetController : BaseController<TypeProjet, TypeProjetDTO, TypeProjetCreateDTO, TypeProjetUpdateDTO, string>
    {
        TypeProjetManager typeprojetmanager;
        public TypeProjetController(TypeProjetManager manager, IMapper mapper) : base(manager, mapper)
        {
            typeprojetmanager = manager;
        }

        protected override int GetEntityId(TypeProjet entity) => entity.Id;

        [HttpPost]
        public override async Task<ActionResult<TypeProjetDTO>> Create([FromBody] TypeProjetCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exists = await typeprojetmanager.GetByNameAsync(dto.Nom);

            if (exists != null)
            {
                ModelState.AddModelError("Nom", "Ce type de projet existe déjà.");
                return Conflict(new { message = $"Le type de projet '{dto.Nom}' existe déjà.", errors = ModelState });
            }

            var entity = _mapper.Map<TypeProjet>(dto);
            var result = await _manager.AddAsync(entity);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, _mapper.Map<TypeProjetDTO>(result));
        }


    }
}
