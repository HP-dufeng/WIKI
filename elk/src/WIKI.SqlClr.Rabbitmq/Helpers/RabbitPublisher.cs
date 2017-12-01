using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace WIKI.SqlClr.Rabbitmq
{
    internal class RabbitPublisher
    {
        internal static void SendMsg(RabbitEndpoint endpoint, string msg)
        {
            var factory = new ConnectionFactory() { HostName = endpoint.ServerName, Port = endpoint.Port, UserName = endpoint.LoginName, Password = endpoint.LoginPassword };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                string exchange = string.IsNullOrEmpty(endpoint.Exchange) ? "" : endpoint.Exchange;
                string exchangeType = string.IsNullOrEmpty(endpoint.ExchangeType) ? "" : endpoint.ExchangeType;
                string routeKey = string.IsNullOrEmpty(endpoint.RoutingKey) ? "" : endpoint.RoutingKey;
                string queue = string.IsNullOrEmpty(endpoint.Queue) ? "" : endpoint.Queue;

                if (queue != "")
                    channel.QueueDeclare(queue: endpoint.Queue,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                if (exchange != "" )
                {
                    channel.ExchangeDeclare(exchange: exchange, type: exchangeType == "" ? "direct" : exchangeType);
                    channel.QueueBind(queue, exchange, routeKey);
                }
                    
                var body = Encoding.GetEncoding("gb2312").GetBytes(msg);
                channel.BasicPublish(exchange: exchange,
                                     routingKey: routeKey,
                                     basicProperties: null,
                                     body: body);
            }
        }

    }
}
