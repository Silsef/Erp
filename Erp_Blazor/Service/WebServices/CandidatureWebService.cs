using Blazored.Toast.Services;
using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Candidature;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class CandidatureWebService : BaseWebService<CandidatureDTO, CandidatureCreateDTO, CandidatureUpdateDTO>, ICandidatureService
    {
        public CandidatureWebService(HttpClient client, IToastService toastService) : base(client, "api/candidature", toastService)
        {
        }

        public async Task<List<CandidatureDTO>> GetByOffreId(int offreId)
        {
            try
            {

                return await _client.GetFromJsonAsync<List<CandidatureDTO>>($"{_endpoint}/GetByOffreId/{offreId}") ?? new List<CandidatureDTO>();
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Impossible de récupérer les entités de l'employé.");
                throw;
            }
        }
    }
}
