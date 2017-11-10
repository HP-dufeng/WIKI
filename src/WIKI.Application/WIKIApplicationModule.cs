using System.Reflection;
using Abp.Modules;

namespace WIKI
{
    [DependsOn(typeof(WIKICoreModule))]
    public class WIKIApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
