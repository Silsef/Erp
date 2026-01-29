using Blazored.Toast.Services;
using Erp_Blazor.Service.Interfaces;
using Shared_Erp.TypeProjet;

namespace Erp_Blazor.Service.WebServices
{
    public class TypeProjetWebService : BaseWebService<TypeProjetDTO, TypeProjetCreateDTO, TypeProjetUpdateDTO>, ITypeProjetService
    {
        public TypeProjetWebService(HttpClient client, IToastService toastService) : base(client, "api/typeprojet", toastService)
        {
        }
    }
}
