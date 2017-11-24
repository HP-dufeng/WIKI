using CommonLibs.Common;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Caching;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using WIKI.Core.Entities;
using WIKI.EntityFramework;

namespace WIKI.WebApi
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseResourceAuthorization(new AuthorizationManager());


            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            // 忽略服务器证书错误
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((sender, certificate, chain, errors) => true);

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = AppSetting.GetString("IdentityServerUrl"),
                RequiredScopes = new[] { "openid", "roles", "profile" },
                TokenProvider = new OAuthBearerAuthenticationProvider
                {
                    OnValidateIdentity = OnValidateIdentityHandle
                },
            });
        }

        private Task OnValidateIdentityHandle(OAuthValidateIdentityContext context)
        {
            var Db = new WIKIDbContext();

            var cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.FromUnixTimeSeconds(60 * 30);

            var userId = Guid.Parse(context.Ticket.Identity.FindFirst("sub").Value);

            var obj = cache.Get(userId.ToString());
            if(obj == null)
            {
                var entity = Db.Account.Where(m => m.WUCC_UserId == userId).FirstOrDefault();
                if (entity == null)
                {
                    entity = new Account
                    {
                        WUCC_UserId = userId,
                        Department = context.Ticket.Identity.Claims.FirstOrDefault(m => m.Type == "SalesDepartment").Value,
                        UserName = context.Ticket.Identity.Name,
                        FullName = context.Ticket.Identity.Claims.FirstOrDefault(m => m.Type == "FullName").Value,
                    };
                    Db.Account.Add(entity);
                }
                else
                {
                    entity.Department = context.Ticket.Identity.Claims.FirstOrDefault(m => m.Type == "SalesDepartment").Value;
                    entity.UserName = context.Ticket.Identity.Name;
                    entity.FullName = context.Ticket.Identity.Claims.FirstOrDefault(m => m.Type == "FullName").Value;
                }

                Db.SaveChanges();

                cache.Add(userId.ToString(), entity, policy);
            }

            

            return Task.FromResult(0);
        }
    }
}