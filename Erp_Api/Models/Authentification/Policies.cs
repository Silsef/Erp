using Microsoft.AspNetCore.Authorization;

namespace Erp_Api.Models.Authentification
{
    public class Policies
    {
        public const string ChefProjet = "Chef de projet"; 
        public const string Technicien = "Technicien";
        public const string RH = "RH";
        public const string Tresorier = "Trésorier";
        public const string Authorized = "Authorized";
        public const string Admin = "Admin";

        public static AuthorizationPolicy Logged()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Authorized).Build();
        }

        public static AuthorizationPolicy AdminLogged()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy ChefProjetPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(ChefProjet) 
                .Build();
        }
    }
}
