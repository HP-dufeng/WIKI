using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WIKI.SqlClr.Rabbitmq
{
    internal class RabbitEndpoint
    {
        public int Id { get; set; }
        public string AliasName { get; set; }

        public string ServerName { get; set; }
        public int Port { get; set; }
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }

        public string Exchange { get; set; }
        public string ExchangeType { get; set; }
        public string RoutingKey { get; set; }

        public string Queue { get; set; }

        public bool IsEnabled { get; set; }

        internal RabbitEndpoint(SqlDataReader dr)
        {
            Id = dr.GetInt32(0);
            AliasName = dr.GetString(1);
            ServerName = dr.GetString(2);
            Port = dr.GetInt32(3);
            LoginName = dr.GetString(4);
            LoginPassword = System.Text.Encoding.Unicode.GetString((byte[])dr[5]);
            Exchange = dr.GetString(6);
            ExchangeType = dr.GetString(7);
            RoutingKey = dr[8] != DBNull.Value ? dr.GetString(8) : null;
            Queue = dr.GetString(9);
            IsEnabled = dr.GetBoolean(10);
        }

        public override string ToString()
        {
            return string.Format(@"
AliasName: {0}
ServerName: {1}, 
Port: {2}, 
LoginName: {3}
Password: {4},
Exchange: {5},
ExchangeType: {6},
RoutingKey: {7},
Queue: {8},
IsEnabled: {9}"
, AliasName, ServerName, Port, LoginName, LoginPassword, Exchange, ExchangeType, RoutingKey, Queue, IsEnabled);
        }
    }
}
