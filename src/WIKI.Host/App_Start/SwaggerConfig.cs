using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Swashbuckle.Application;
using System.Reflection;

namespace WIKI.Host
{
    public class SwaggerConfig
    {
        public static void ConfigureSwaggerUi(HttpConfiguration httpConfiguration)
        {
            httpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "WIKI.WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(WIKIWebApiModule)), "WIKI.Api.Scripts.Swagger-Custom.js");
                });
        }
    }
}