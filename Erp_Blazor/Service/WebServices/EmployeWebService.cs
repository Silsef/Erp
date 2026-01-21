using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Employe;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class EmployeWebService : BaseWebService<EmployeDTO, EmployeCreateDTO, EmployeUpdateDTO>, IEmployeService
    {
        public EmployeWebService(HttpClient client) : base(client, "api/employe")
        {
        }

        public async Task<EmployeDTO> GetByNom(string Nom)
        {
            return await _client.GetFromJsonAsync<EmployeDTO>($"{_endpoint}/GetByNom/{Nom}");
        }
    }
}
