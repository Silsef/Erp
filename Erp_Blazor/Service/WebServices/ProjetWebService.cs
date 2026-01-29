using Blazored.Toast.Services;
using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Projet;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class ProjetWebService : BaseWebService<ProjetDTO, ProjetCreateDTO, ProjetUpdateDTO>, IProjetService
    {
        public ProjetWebService(HttpClient client, IToastService toastService) : base(client, "api/projet", toastService)
        {

        }

        public async Task<List<ProjetDTO>> GetProjetsInternesByEntiteId(int entiteId)
        {
            try
            {
                var resultat = await _client.GetFromJsonAsync<List<ProjetDTO>>($"{_endpoint}/GetProjetsInternesByEntiteId/{entiteId}");
                return resultat ?? new List<ProjetDTO>();
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Impossible de récupérer les projets internes de l'entite.");
                throw;
            }
        }
        public async Task<List<ProjetDTO>> GetProjetsExternesByEntiteId(int entiteId)
        {
            try
            {

                var resultat = await _client.GetFromJsonAsync<List<ProjetDTO>>($"{_endpoint}/GetProjetsExternesByEntiteId/{entiteId}");
                return resultat ?? new List<ProjetDTO>();
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Impossible de récupérer les projets externes de l'entite.");
                throw;
            }
        }
        
        public async Task<List<ProjetDTO>> GetProjetsByEmployeId(int employeid)
        {
            try
            {
                var resultat = await _client.GetFromJsonAsync<List<ProjetDTO>>($"{_endpoint}/GetProjetsByEmployeId/{employeid}");
                return resultat ?? new List<ProjetDTO>();
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Impossible de récupérer les projets de l'employé.");
                throw;
            }
        }

    }
}
