using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Entretien;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class EntretienWebService : BaseWebService<EntretienDTO, EntretienCreateDTO, EntretienUpdateDTO>, IEntretienService
    {
        public EntretienWebService(HttpClient client) : base(client, "api/entretien")
        {
        }

        public async Task<List<EntretienDTO>> GetByOffreId(int offreId)
        {
            return await _client.GetFromJsonAsync<List<EntretienDTO>>($"{_endpoint}/GetByOffreId/{offreId}") ?? new List<EntretienDTO>();
        }
    }
}
