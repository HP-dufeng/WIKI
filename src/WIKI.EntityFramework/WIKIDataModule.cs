using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using WIKI.EntityFramework;

namespace WIKI
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(WIKICoreModule))]
    public class WIKIDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<WIKIDbContext>(null);
        }
    }
}
