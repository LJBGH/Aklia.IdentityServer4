using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aklia.IdentityServer.Application.Identitys
{
    public class Config
    {
        /// <summary>
        ///     定义要保护的资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
               new ApiResource("api1","MyApi"),
               new ApiResource("ocelot","test ocelot")
            };
        }

        /// <summary>
        ///     定义授权客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client(){
                    ClientId="client",//客户端的标识，要惟一的
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,//授权方式，必须要用户名和密码
                    ClientSecrets=
                    {
                      new Secret("secret".Sha256())
                    },
                    //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                    AllowedScopes=//定义这个客户端可以访问的API资源数组
                    { "api1"
                       //IdentityServerConstants.StandardScopes.OfflineAccess
                    }
                    //AllowOfflineAccess=true// 主要刷新refresh_token
                }
            };
        }
    }
}
