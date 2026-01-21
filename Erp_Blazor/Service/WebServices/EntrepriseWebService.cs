using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Entreprise;

namespace Erp_Blazor.Service.WebServices
{
    public class EntrepriseWebService : BaseWebService<EntrepriseDTO, EntrepriseCreateDTO, EntrepriseUpdateDTO>, IEntrepriseService
    {
        public EntrepriseWebService(HttpClient client) : base(client, "api/entreprise")
        {
        }
    }
}
