using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Offre;

namespace Erp_Blazor.Service.WebServices
{
    public class OffreWebService : BaseWebService<OffreDTO, OffreCreateDTO, OffreUpdateDTO>, IOffreService
    {
        public OffreWebService(HttpClient client) : base(client, "api/Offre")
        {
        }
    }
}
