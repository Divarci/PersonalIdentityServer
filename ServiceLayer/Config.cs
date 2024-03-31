using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer.ServiceLayer;

public static class Config
{
    //korunacak apiler burada belirlenir. scopelarida burada belirlenir
    public static IEnumerable<ApiResource> ApiResources()
    {
        return new List<ApiResource>()
            {
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
                //new ("IdentityApi"){Scopes={ "IdentityApi.Admin", "IdentityApi.Member"}}
            };
    }

    //korunan apilerin belirlenen scopelarin detaylari burada belirlenir
    public static IEnumerable<ApiScope> ApiScopes()
    {
        return new List<ApiScope>()
        {
            new (IdentityServerConstants.LocalApi.ScopeName),
            //new("IdentityApi.Admin","Access for admin area"),
            //new("IdentityApi.Member","Access for member area"),
        };
    }

    //token icinde olmasi gereken bilgiler
    public static IEnumerable<IdentityResource> IdentityResources()
    {
        return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(){Name="Roles",DisplayName="Roles",Description="User Roles",UserClaims=new[]{"role"}}

            };
    }
    
    //client belirlenir
    public static IEnumerable<Client> Clients()
    {
        return new List<Client>()
            {
                new Client
                {
                    ClientId = "CW",
                    ClientName = "CompanyWebPage",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = { new Secret("kakaLEYTO12*".Sha256()) },

                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess,"Roles",IdentityServerConstants.LocalApi.ScopeName },

                    AccessTokenLifetime = (int)(DateTime.Now.AddDays(1)-DateTime.Now).TotalSeconds,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AbsoluteRefreshTokenLifetime =(int)(DateTime.Now.AddDays(30)-DateTime.Now).TotalSeconds,
                    RefreshTokenExpiration = TokenExpiration.Absolute,

                },
                new Client
                {
                    ClientId = "Test",
                    ClientName = "TestClient",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = { new Secret("kakaLEYTO12*".Sha256()) },

                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess,"Roles",IdentityServerConstants.LocalApi.ScopeName },

                    AccessTokenLifetime = (int)(DateTime.Now.AddDays(1)-DateTime.Now).TotalSeconds,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AbsoluteRefreshTokenLifetime =(int)(DateTime.Now.AddDays(30)-DateTime.Now).TotalSeconds,
                    RefreshTokenExpiration = TokenExpiration.Absolute,

                }
            };
    }
   

}
