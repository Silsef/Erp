using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Entite;

namespace Erp_Blazor.Service.WebServices
{
    public class EntiteWebService : BaseWebService<EntiteDTO, EntiteCreateDTO, EntiteUpdateDTO>, IEntiteService
    {
        public EntiteWebService(HttpClient client) : base(client, "api/entite")
        {
        }
    }
}
