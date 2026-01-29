using Blazored.Toast.Services;
using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Employe;
using Shared_Erp.Entite;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class EntiteWebService : BaseWebService<EntiteDTO, EntiteCreateDTO, EntiteUpdateDTO>, IEntiteService
    {
        public EntiteWebService(HttpClient client, IToastService toastService) : base(client, "api/entite", toastService)
        {
        }
       public async Task<List<EntiteDTO>> GetByIdEmploye(int idemp)
        {
            try 
            { 
                return await _client.GetFromJsonAsync<List<EntiteDTO>>($"{_endpoint}/GetEntiteByIdEmploye/{idemp}");
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Impossible de récupérer les entites de l'employe.");
                throw;
            }
        }
    }
}

