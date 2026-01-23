using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Entretien;
using Shared_Erp.Tache;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class TacheWebService : BaseWebService<TacheDTO, TacheCreateDTO, TacheUpdateDTO>, ITacheService
    {
        public TacheWebService(HttpClient client) : base(client, "api/tache")
        {
        }

        public async Task<List<TacheDTO>> GetTachesByProjetId(int idprojet)
        {
            return await _client.GetFromJsonAsync<List<TacheDTO>>($"{_endpoint}/GetTachesByProjetId/{idprojet}") ?? new List<TacheDTO>();

        }
    }
}
