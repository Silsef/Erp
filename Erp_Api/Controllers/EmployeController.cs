using AutoMapper;
using Erp_Api.Models.Entity;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Repository.Interfaces;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Npgsql;
using Shared_Erp.Employe;
using Shared_Erp.Projet;
using Shared_Erp.Tache;
using System.Data.Entity.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Erp_Api.Controllers
{
    public class EmployeController : BaseController<Employe, EmployeDTO, EmployeCreateDTO, EmployeUpdateDTO, string>
    {
        public EmployeManager employeManager;
        public EmployeController(EmployeManager manager, IMapper mapper)
            : base(manager, mapper)
        {
            employeManager = manager;
        }

        protected override int GetEntityId(Employe entity) => entity.Id;

        [HttpGet("{identite}")]
        [HttpGet("{identite}/{idprojet}")]
        public async Task<ActionResult<IEnumerable<EmployeDTO>>> GetEmployeesByEntiteEtPProjet(int identite,int? idprojet)
        {
            var taches = await employeManager.GetEmployeesByEntiteEtProjet(identite,idprojet);

            return Ok(_mapper.Map<IEnumerable<EmployeDTO>>(taches));
        }
    }
}
