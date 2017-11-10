using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace WIKI
{
    [DependsOn(typeof(AbpWebApiModule), typeof(WIKIApplicationModule))]
    public class WIKIWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(WIKIApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
