using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Projet;

namespace Erp_Blazor.Service.WebServices
{
    public class ProjetWebServic : BaseWebService<ProjetDTO, ProjetCreateDTO, ProjetUpdateDTO>, IProjetService
    {
        public ProjetWebServic(HttpClient client) : base(client, "api/projet")
        {
        }
    }
}
