using Abp.Application.Services;

namespace WIKI
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class WIKIAppServiceBase : ApplicationService
    {
        protected WIKIAppServiceBase()
        {
            LocalizationSourceName = WIKIConsts.LocalizationSourceName;
        }
    }
}