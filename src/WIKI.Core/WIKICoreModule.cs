using System.Reflection;
using Abp.Modules;

namespace WIKI
{
    public class WIKICoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
