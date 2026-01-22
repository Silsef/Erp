using AutoMapper;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Shared_Erp.Tache;

namespace Erp_Api.Controllers
{
    public class TacheController : BaseController<Tache, TacheDTO, TacheCreateDTO, TacheUpdateDTO, string>
    {
        public TacheController(TacheManager manager, IMapper mapper)
            : base(manager, mapper)
        {
        }
        protected override int GetEntityId(Tache entity) => entity.Id;
   
    
    }
}
