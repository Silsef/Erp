using Blazored.Toast.Services;
using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Entretien;
using Shared_Erp.Tache;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class TacheWebService : BaseWebService<TacheDTO, TacheCreateDTO, TacheUpdateDTO>, ITacheService
    {
        public TacheWebService(HttpClient client, IToastService toastService) : base(client, "api/tache", toastService)
        {
        }

        public async Task<List<TacheDTO>> GetTachesByProjetId(int idprojet)
        {
            try
            {
                return await _client.GetFromJsonAsync<List<TacheDTO>>($"{_endpoint}/GetTachesByProjetId/{idprojet}") ?? new List<TacheDTO>();
            }
            catch (Exception ex) 
            {
                _toastService.ShowError("Impossible de récupérer les taches du projet.");
                throw;
            }

        }
    }
}
