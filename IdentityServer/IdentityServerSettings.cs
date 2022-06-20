using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class IdentityServerSettings
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("eventPlanning_api", "EventPlanningAPI")
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
                new ApiResource("eventPlanning_api", "EventPlanningAPI")
                {
                    Scopes = { "eventPlanning_api" }
                }
            };

        public static IEnumerable<Client> Clients (ICollection<string> urls) =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "swagger_api",
                    ClientName = "Swagger API",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowedScopes = {"eventPlanning_api"},
                    AllowedCorsOrigins = urls,
                    AccessTokenLifetime = 86400,
                },
                new Client
                {
                    ClientId = "planningFront",
                    ClientName = "Planning Front",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowedScopes = {"eventPlanning_api"},
                    AllowedCorsOrigins = urls,
                    AccessTokenLifetime = 86400,
                }
            };
    }
}
