using MassTransit;
using Serilog;
using System;
using System.Configuration;
using System.ServiceProcess;
using WIKI.MassTransit.Consumer.UploadEventConsumer;

namespace WIKI.WindowsService
{
    public partial class ProgramService : ServiceBase
    {
        IBusControl _busControl;

        static void Main(string[] args)
        {
            // 日志
            Log.Logger = new LoggerConfiguration()
              .ReadFrom.AppSettings()
              .CreateLogger();

            using (var service = new ProgramService())
            {
                // 如果是命令行启动
                if (Environment.UserInteractive)
                {
                    service.OnStart(null);

                    Console.Title = "MassTransit.WIKI.Subscriber";
                    Console.WriteLine("总线已启动. 按任意键退出");

                    Console.ReadKey();
                    service.OnStop();
                    return;
                }

                Run(service);
            }
        }

        protected override void OnStart(string[] args)
        {
            BusOnStart();
        }

        protected override void OnStop()
        {
            if (_busControl != null)
            {
                _busControl.Stop();
            }
        }

        void BusOnStart()
        {
            string url = ConfigurationManager.AppSettings["RabbitMq_Server"].ToString();
            string user = ConfigurationManager.AppSettings["RabbitMq_User"].ToString();
            string password = ConfigurationManager.AppSettings["RabbitMq_Password"].ToString();

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(url), h =>
                {
                    h.Username(user);
                    h.Password(password);
                });

                cfg.ReceiveEndpoint(host, "WIKI_UploadEvent_Queue", e =>
                {
                    e.Consumer<UploadAttachmentEventConsumer>();
                });

               
            });

            _busControl.Start();
        }
    }
}
