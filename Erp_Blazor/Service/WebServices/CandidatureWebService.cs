using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Candidature;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class CandidatureWebService : BaseWebService<CandidatureDTO, CandidatureCreateDTO, CandidatureUpdateDTO>, ICandidatureService
    {
        public CandidatureWebService(HttpClient client) : base(client, "api/candidature")
        {
        }

        public async Task<List<CandidatureDTO>> GetByOffreId(int offreId)
        {
            return await _client.GetFromJsonAsync<List<CandidatureDTO>>($"{_endpoint}/GetByOffreId/{offreId}") ?? new List<CandidatureDTO>();
        }
    }
}
