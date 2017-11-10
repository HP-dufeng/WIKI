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
using Abp.Runtime.Caching;

[assembly: OwinStartup(typeof(WIKI.Host.Startup))]
namespace WIKI.Host
{
    public partial class Startup
    {
        private AbpBootstrapper _abpBootstrapper;
        private ICacheManager _cacheManager;

        public void Configuration(IAppBuilder app)
        {
            _abpBootstrapper = AbpBootstrapper.Create<WIKIWebModule>();
            _abpBootstrapper.Initialize();

            _abpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(HttpContext.Current.Server.MapPath("log4net.config"))
            );

            _cacheManager = _abpBootstrapper.IocManager.Resolve<ICacheManager>();

            ConfigureAuth(app);

            var httpConfig = _abpBootstrapper.IocManager.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;
            app.UseWebApi(httpConfig);
        }


    }
}