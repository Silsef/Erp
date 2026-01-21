using AutoMapper;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erp_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProjetController: ControllerBase
    {
        private readonly EntretienManager _manager;
        private readonly IMapper _mapper;

        public ProjetController(EntretienManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }


    }
}
