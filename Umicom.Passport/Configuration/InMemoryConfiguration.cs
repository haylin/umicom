using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;

namespace Umicom.Passport
{
    public class InMemoryConfiguration
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("clientservice", "CAS Client Service"),
                new ApiResource("productservice", "CAS Product Service"),
                new ApiResource("agentservice", "CAS Agent Service")
            };
        }

        /// <summary>
        /// Define which Apps will use thie IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "client.api.service",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientservice" }
                },
                new Client
                {
                    ClientId = "product.api.service",
                    ClientSecrets = new [] { new Secret("productsecret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientservice", "productservice" }
                },
                new Client
                {
                    ClientId = "agent.api.service",
                    ClientSecrets = new [] { new Secret("agentsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "agentservice", "clientservice", "productservice" }
                },
                new Client
                {
                    ClientId = "cas.mvc.client.implicit",
                    ClientName = "CAS MVC Web App Client",
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    RedirectUris = { $"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signin-oidc" },
                    PostLogoutRedirectUris = { $"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signout-callback-oidc" },
                    AllowedScopes = new [] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "agentservice", "clientservice", "productservice"
                    },
                    RequireConsent=false,
                    AccessTokenLifetime = 60, // one hour
                    AllowAccessTokensViaBrowser = true // can return access_token to this client
                },
              new Client
                 {
                     ClientId = "mvc",
                     // no interactive user, use the clientid/secret for authentication
                     AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,  //简化模式
                     // secret for authentication
                     ClientSecrets =
                     {
                         new Secret("secret".Sha256())
                     },
                     RequireConsent =true,                                  //用户选择同意认证授权
                     //指定允许的URI返回令牌或授权码(我们的客户端地址)
                     RedirectUris = { $"http://{Configuration["Clients:MVC_Client:IP"]}:{Configuration["Clients:MVC_Client:Port"]}/signin-oidc" },
                     //注销后重定向地址 参考https://identityserver4.readthedocs.io/en/release/reference/client.html
                     PostLogoutRedirectUris = { $"http://{Configuration["Clients:MVC_Client:IP"]}:{Configuration["Clients:MVC_Client:Port"]}/signout-callback-oidc" },
                     LogoUri="https://ss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=3298365745,618961144&fm=27&gp=0.jpg",
                     // scopes that client has access to
                     AllowedScopes = {                       //客户端允许访问个人信息资源的范围
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Email,
                         IdentityServerConstants.StandardScopes.Address,
                         IdentityServerConstants.StandardScopes.Phone,
                         IdentityServerConstants.StandardScopes.OfflineAccess
                     },
                    AccessTokenLifetime = 30, // one hour
                    UpdateAccessTokenClaimsOnRefresh=true,
                    AccessTokenType=AccessTokenType.Jwt,
                    AllowAccessTokensViaBrowser = true // can return access_token to this client
                 },
              new Client
                {
                    ClientId = "mvc1",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris           = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    AllowOfflineAccess = true
                },
              new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC 客户端",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                },
        };
        }

        /// <summary>
        /// Define which uses will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "10001",
                    Username = "test001",
                    Password = "test001",
                    Claims =new List<Claim>{ new Claim( JwtClaimTypes.Role,"superadmin") }
                },
                new TestUser
                {
                    SubjectId = "10002",
                    Username = "test002",
                    Password = "test002",
                     Claims =new List<Claim>{ new Claim( JwtClaimTypes.Role,"admin") }
                },
                new TestUser
                {
                    SubjectId = "10003",
                    Username = "test003",
                    Password = "test003",
                    Claims =new List<Claim>{ new Claim( JwtClaimTypes.Role,"user") }
                }
            };
        }

        /// <summary>
        /// Define which IdentityResources will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone()
            };
        }
    }
}