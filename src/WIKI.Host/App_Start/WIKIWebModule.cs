using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Configuration.Startup;
using System.Web.Http;
using Abp.Web.Mvc;
using Swashbuckle.Application;
using System.Linq;

namespace WIKI.Host
{
    [DependsOn(
        typeof(AbpWebMvcModule),
        typeof(WIKIDataModule), 
        typeof(WIKIApplicationModule), 
        typeof(WIKIWebApiModule))]
    public class WIKIWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration = new HttpConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            var httpConfiguration = Configuration.Modules.AbpWebApi().HttpConfiguration;

            RouteConfig.RegisterRoutes(httpConfiguration.Routes);

            SwaggerConfig.ConfigureSwaggerUi(httpConfiguration);
        }

    }
}
