using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;
using Serilog.Events;
using System.Web.Hosting;
using Serilog.Exceptions;
using SerilogWeb.Classic.Enrichers;
using System.IO;

namespace WIKI.WebApi.Models
{
    public class Logger
    {
        private static ILogger _logger;
        public static ILogger Instance
        {
            get
            {
                if (_logger == null)
                {
                    _logger = CreateLogger();
                }

                return _logger;
            }
        }

        public static void Init()
        {
            _logger = CreateLogger();
        }

        private static ILogger CreateLogger()
        {
            string logpath = HostingEnvironment.MapPath("~");

            //日志 根据web.config中的配置进行记录
            var logger = new LoggerConfiguration()
            .MinimumLevel.Is(LogEventLevel.Error)
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ApplicationName", "rbs")
            .Enrich.With<HttpRequestClientHostIPEnricher>()
            .Enrich.With<HttpRequestRawUrlEnricher>()
            .Enrich.With<HttpRequestIdEnricher>()
            .Enrich.With<UserNameEnricher>()
            //.Enrich.WithProperty("RuntimeVersion", Environment.Version)
            .Enrich.FromLogContext()
            .MinimumLevel.Verbose()
            .WriteTo.RollingFile(
                Path.Combine(logpath, "Logs\\Error-{Date}.log")
                , retainedFileCountLimit: (int)LogEventLevel.Error
                , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {SourceContext} - ({MachineName}|{HttpRequestId}|{UserName}) {Message}{NewLine}{Exception}"
            )
            .CreateLogger();

            return logger;
        }
    }
}