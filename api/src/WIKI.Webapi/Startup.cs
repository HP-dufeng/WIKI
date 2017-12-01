using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using IdentityServer3.AccessTokenValidation;
using System.Security.Cryptography.X509Certificates;
using Owin;
using System.Net;
using System.Net.Security;
using Serilog;
using System.IdentityModel.Tokens;
using CommonLibs.Common;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Routing;
using MassTransit;
using Autofac.Integration.WebApi;
using WIKI.WebApi;
using MassTransit.Util;
using Microsoft.Owin.BuilderProperties;
using System.Threading;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Collections.ObjectModel;
using System.IO;
using System.Web.Http;

[assembly: OwinStartup(typeof(WIKI.WebApi.Startup))]

namespace WIKI.WebApi
{
    public partial class Startup
    {
        private HttpConfiguration _config;

        public void Configuration(IAppBuilder app)
        {
            _config = new HttpConfiguration();

            ConfigureLog();

            ConfigureAuth(app);

            ConfigureWebApi(app);

        }


    }
}
