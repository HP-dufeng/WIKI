using CommonLibs.Common;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WIKI.WebApi.Filter;

namespace WIKI.WebApi
{
    public partial class Startup
    {
        private void ConfigureWebApi(IAppBuilder app)
        {
            // Web API 配置和服务
            _config.Filters.Add(new CatchExceptionAttribute());
            //_config.Filters.Add(new OpLogAttribute(new SqlServerProvider()));

            //允许接入的客户端
            string enableCorsUrl = AppSetting.GetString("EnableCorsUrl");
            _config.EnableCors(new EnableCorsAttribute(enableCorsUrl, "*", "*", "WWW-Authenticate,Content-Disposition"));


            // 忽略循环引用
            _config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Web API 路由
            _config.MapHttpAttributeRoutes();

            _config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            ODataConfig.RegisterOData(_config);

            app.UseWebApi(_config);
        }
    }
}