using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Projet;

namespace Erp_Blazor.Service.WebServices
{
    public class ProjetWebService : BaseWebService<ProjetDTO, ProjetCreateDTO, ProjetUpdateDTO>, IProjetService
    {
        public ProjetWebService(HttpClient client) : base(client, "api/projet")
        {
        }
    }
}
