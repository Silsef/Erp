using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Employe;
using Shared_Erp.Entite;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class EntiteWebService : BaseWebService<EntiteDTO, EntiteCreateDTO, EntiteUpdateDTO>, IEntiteService
    {
        public EntiteWebService(HttpClient client) : base(client, "api/entite")
        {
        }
             public async Task<List<EntiteDTO>> GetByIdEmploye(int idemp)
            {
                return await _client.GetFromJsonAsync<List<EntiteDTO>>($"{_endpoint}/GetEntiteByIdEmploye/{idemp}");
            }
    }
}

