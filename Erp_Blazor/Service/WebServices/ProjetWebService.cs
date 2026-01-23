using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Projet;
using System.Net.Http.Json;

namespace Erp_Blazor.Service.WebServices
{
    public class ProjetWebService : BaseWebService<ProjetDTO, ProjetCreateDTO, ProjetUpdateDTO>, IProjetService
    {
        public ProjetWebService(HttpClient client) : base(client, "api/projet")
        {

        }

        public async Task<List<ProjetDTO>> GetProjetsInternesByEntiteId(int entiteId)
        {
            var resultat = await _client.GetFromJsonAsync<List<ProjetDTO>>($"{_endpoint}/GetProjetsInternesByEntiteId/{entiteId}");
            return resultat ?? new List<ProjetDTO>();
        }
        public async Task<List<ProjetDTO>> GetProjetsExternesByEntiteId(int entiteId)
        {
            var resultat = await _client.GetFromJsonAsync<List<ProjetDTO>>($"{_endpoint}/GetProjetsExternesByEntiteId/{entiteId}");
            return resultat ?? new List<ProjetDTO>();
        }
    }
}
