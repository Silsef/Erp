using Blazored.Toast.Services;
using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Employe;
using Shared_Erp.Entite;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class EmployeWebService : BaseWebService<EmployeDTO, EmployeCreateDTO, EmployeUpdateDTO>, IEmployeService
    {
        public EmployeWebService(HttpClient client,IToastService toastService) : base(client, "api/employe",toastService)
        {
        }

        public async Task<EmployeDTO> GetByNom(string Nom)
        {
            try
            {
                return await _client.GetFromJsonAsync<EmployeDTO>($"{_endpoint}/GetByNom/{Nom}");
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Impossible de récupérer l'employé.");
                throw;
            }
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

            try
            {
                return await _client.GetFromJsonAsync<List<EmployeDTO>>(url);
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Impossible de récupérer les employés.");
                throw;
            }
        }
    }
}