using MassTransit;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WIKI.Question.Consumer;

namespace WIKI.Question.Service
{
    public partial class ProgramService : ServiceBase
    {
        EventBus bus;

        static void Main(string[] args)
        {
            // 日志
            Log.Logger = new LoggerConfiguration()
              .ReadFrom.AppSettings()
              .CreateLogger();

            using (var service = new ProgramService())
            {

                Run(service);
            }
        }

        public ProgramService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Serilog.Log.Information("WIKI.Question.Service start");

            bus = new EventBus();

            try
            {
                bus.Start();
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                throw ex;
            }
            
        }

        protected override void OnStop()
        {
            Log.Information("WIKI.Question.Service stop");

            if (bus != null)
            {
                bus.Stop();
            }
        }
    }
}
