using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace SchoolIsComingSoon.Identity
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("SchoolIsComingSoonWebAPI", "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("SchoolIsComingSoonWebAPI", "Web API", new []
                { 
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    JwtClaimTypes.Email
                })
                {
                    Scopes = { "SchoolIsComingSoonWebAPI" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "schooliscomingsoon-web-app",
                    ClientName = "SchoolIsComingSoon Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://localhost:3000/signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:3000"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:3000/signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.GivenName,
                        JwtClaimTypes.FamilyName,
                        JwtClaimTypes.Email,
                        "SchoolIsComingSoonWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}