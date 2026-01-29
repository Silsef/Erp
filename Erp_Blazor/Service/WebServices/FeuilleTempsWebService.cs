using Blazored.Toast.Services;
using Erp_Blazor.Service.Interfaces;
using Shared_Erp.FeuilleTemps;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class FeuilleTempsWebService : BaseWebService<FeuilleTempsDTO, FeuilleTempsCreateDTO, FeuilleTempsUpdateDTO>, IFeuilleTempsService
    {
        public FeuilleTempsWebService(HttpClient client, IToastService toastService) : base(client, "api/feuilletemps", toastService)
        {
        }

        public async Task<List<FeuilleTempsDTO>> GetBySemaine(int employeId, int numSemaine, int? annee = null, int? projetId = null)
        {
            string url;

            if (employeId == 0)
            {
                url = $"{_endpoint}/GetBySemaine/{numSemaine}?"; 
            }
            else
            {
                url = $"{_endpoint}/GetBySemaine/{employeId}/{numSemaine}?";
            }

            if (annee.HasValue)
            {
                url += $"annee={annee.Value}&";
            }

            if (projetId.HasValue)
            {
                url += $"projetId={projetId.Value}&";
            }

            url = url.TrimEnd('?', '&');
            try
            {

                var resultat = await _client.GetFromJsonAsync<List<FeuilleTempsDTO>>(url);

                return resultat ?? new List<FeuilleTempsDTO>();
            }
            catch (Exception ex)
            {
                _toastService.ShowError("Impossible de récupérer les feuilles de temps.");
                throw;
            }
        }
    }
}