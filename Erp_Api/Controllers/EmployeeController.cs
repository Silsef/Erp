using System.Data.Entity.Infrastructure;
using Erp_Api.Models.Entity;
using Erp_Api.Models.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using MailKit.Net.Smtp;
using MimeKit;
using Npgsql;
using Microsoft.AspNetCore.Http.HttpResults;
using Erp_Api.Models.Entity.Tables.Entitees;
using Shared_Erp.Employe;

namespace Erp_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController(EmployeManager _manager,IMapper _mapper) : ControllerBase
    {
        [ActionName("GetById")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeDTO>> GetByID(int id)
        {
            var result = await _manager.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return _mapper.Map<EmployeDTO>(result);
        }
    }
}
