using Abp.WebApi.Authorization;
using Abp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Controllers
{
    public class DemoController:AbpApiController
    {
        //[AbpApiAuthorize]
        public object Get()
        {
            var userId = AbpSession.UserId;

            Logger.Debug("This is a test msg");
            return new { msg = "this is a test" };
        }
    }
}
