using Blazored.Toast.Services;
using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Entretien;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class EntretienWebService : BaseWebService<EntretienDTO, EntretienCreateDTO, EntretienUpdateDTO>, IEntretienService
    {
        public EntretienWebService(HttpClient client, IToastService toastService) : base(client, "api/entretien", toastService)
        {
        }

        public async Task<List<EntretienDTO>> GetByOffreId(int offreId)
        {
            try
            {

                return await _client.GetFromJsonAsync<List<EntretienDTO>>($"{_endpoint}/GetByOffreId/{offreId}") ?? new List<EntretienDTO>();
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Impossible de récupérer les entretiens de l'offre.");
                throw;
            }
        }
    }
}
