using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace SchoolIsComingSoon.Identity
{
    public static class Configuration
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
                new ApiResource("SchoolIsComingSoonWebAPI", "Web API")
                {
                    Scopes = { "SchoolIsComingSoonWebAPI" },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.GivenName,
                        JwtClaimTypes.FamilyName,
                        JwtClaimTypes.Email
                    }
                }
            };

        public static IEnumerable<Client> Clients(string clientUrl) =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "schooliscomingsoon-web-app",
                    ClientName = "SchoolIsComingSoon Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,

                    RedirectUris = { $"{clientUrl}/signin-oidc" },
                    PostLogoutRedirectUris = { $"{clientUrl}/signout-oidc", $"{clientUrl}/silent-renew.html" },
                    AllowedCorsOrigins = { clientUrl },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "SchoolIsComingSoonWebAPI",
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },

                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AccessTokenLifetime = 3600,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 60 * 60 * 24,
                    AbsoluteRefreshTokenLifetime = 30 * 24 * 60 * 60,
                    AccessTokenType = AccessTokenType.Jwt
                }
            };
    }
}