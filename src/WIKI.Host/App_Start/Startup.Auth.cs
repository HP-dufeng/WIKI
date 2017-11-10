using Castle.Facilities.Logging;
using Abp.Castle.Logging.Log4Net;
using Abp.WebApi.Configuration;
using Abp;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.AccessTokenValidation;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Owin.BuilderProperties;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Security;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using WIKI.Users;

namespace WIKI.Host
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            // 忽略服务器证书错误
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((sender, certificate, chain, errors) => true);

            var identityOptions = new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://wucc.wdqh.com:6789/identity",
                RequiredScopes = new[] { "openid", "roles", "profile" },
                TokenProvider = new OAuthBearerAuthenticationProvider
                {
                    OnValidateIdentity = OnValidateIdentityHandle
                },
            };

            app.UseIdentityServerBearerTokenAuthentication(identityOptions);
        }

        private Task OnValidateIdentityHandle(OAuthValidateIdentityContext context)
        {
            var userAppService = _abpBootstrapper.IocManager.Resolve<IUserAppService>();

            var originalUserId = Guid.Parse(context.Ticket.Identity.FindFirst("sub").Value);

            var userId = _cacheManager
                .GetCache<Guid, long?>("UserId")
                .Get(originalUserId, () => userAppService.InsertAndGetId(new Users.Dtos.CreateUserInput { WUCCUserId = originalUserId }));

            context.Ticket.Identity.AddClaim(new System.Security.Claims.Claim(AbpClaimTypes.UserId, userId.ToString()));

            return Task.FromResult(0);
        }

    }

}