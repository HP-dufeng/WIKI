using CommonLibs.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WIKI.WebApi
{
    public partial class Startup
    {
        private void ConfigureLog()
        {
            Serilog.Debugging.SelfLog.Enable((msg) =>
            {
                File.AppendAllText(AppSetting.GetString("SerilogDebugLogPath"), msg);
            });

        }
    }
}