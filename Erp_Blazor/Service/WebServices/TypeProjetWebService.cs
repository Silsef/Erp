using Erp_Blazor.Service.Interfaces;
using Shared_Erp.TypeProjet;

namespace Erp_Blazor.Service.WebServices
{
    public class TypeProjetWebService : BaseWebService<TypeProjetDTO, TypeProjetCreateDTO, TypeProjetUpdateDTO>, ITypeProjetService
    {
        public TypeProjetWebService(HttpClient client) : base(client, "api/typeprojet")
        {
        }
    }
}
