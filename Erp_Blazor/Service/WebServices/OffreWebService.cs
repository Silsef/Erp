using Blazored.Toast.Services;
using Erp_Blazor.Service.Interfaces;
using Shared_Erp.Offre;

namespace Erp_Blazor.Service.WebServices
{
    public class OffreWebService : BaseWebService<OffreDTO, OffreCreateDTO, OffreUpdateDTO>, IOffreService
    {
        public OffreWebService(HttpClient client, IToastService toastService) : base(client, "api/offre", toastService)
        {
        }
    }
}
