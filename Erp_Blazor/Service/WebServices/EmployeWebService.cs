using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Employe;
using Shared_Erp.Entite;
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

        public async Task<List<EmployeDTO>> GetEmployeesByEntiteEtProjet(int? identite, int? idproj)
        {
            string url;

            if (idproj.HasValue)
            {
                url = $"{_endpoint}/GetEmployeesByEntiteEtProjet/{identite}/{idproj.Value}";
            }
            else
            {
                url = $"{_endpoint}/GetEmployeesByEntiteEtProjet/{identite}";
            }

            return await _client.GetFromJsonAsync<List<EmployeDTO>>(url);
        }
    }
}