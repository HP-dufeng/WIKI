using MassTransit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Question.Consumer
{
    public class EventBus
    {
        private string url;
        private string user;
        private string password;

        private IBusControl _busControl;

        public EventBus()
        {
            url = ConfigurationManager.AppSettings["RabbitMq_Server"].ToString();
            user = ConfigurationManager.AppSettings["RabbitMq_User"].ToString();
            password = ConfigurationManager.AppSettings["RabbitMq_Password"].ToString();


        }

        public void Start()
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(url), h =>
                {
                    h.Username(user);
                    h.Password(password);
                });

                

                cfg.ReceiveEndpoint(host, "WIKI_Queue_Question", e =>
                {
                    
                    e.PrefetchCount = 1;
                    e.UseJsonSerializer();
                    e.Consumer<QuestionConsumer>();
                });

                cfg.ReceiveEndpoint(host, "WIKI_Queue_Answer", e =>
                {

                    e.PrefetchCount = 1;
                    e.UseJsonSerializer();
                    e.Consumer<AnswerConsumer>();
                });

            });

            _busControl.Start();

        }

        public void Stop()
        {
            if (_busControl != null)
            {
                _busControl.Stop();
            }
        }


    }
}
