using Erp_Blazor.Service.Interfaces;
using Shared_Erp.FeuilleTemps;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class FeuilleTempsWebService : BaseWebService<FeuilleTempsDTO, FeuilleTempsCreateDTO, FeuilleTempsUpdateDTO>, IFeuilleTempsService
    {
        public FeuilleTempsWebService(HttpClient client) : base(client, "api/feuilletemps")
        {
        }

        public async Task<List<FeuilleTempsDTO>> GetBySemaine(int employeId, int numSemaine, int? annee = null)
        {
            var url = $"{_endpoint}/GetBySemaine/{employeId}/{numSemaine}";

            if (annee.HasValue)
            {
                url += $"?annee={annee.Value}";
            }

            var resultat = await _client.GetFromJsonAsync<List<FeuilleTempsDTO>>(url);

            return resultat ?? new List<FeuilleTempsDTO>();
        }
    }
}
