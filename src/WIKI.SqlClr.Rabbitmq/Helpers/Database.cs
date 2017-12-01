using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WIKI.SqlClr.Rabbitmq
{
    internal class Database
    {
        internal static RabbitEndpoint GetEndpointConfig(string endpointName)
        {
            RabbitEndpoint endpoint = null;
            try
            {
                var sql = string.Format("select * from rmq.tb_RabbitEndpoint with(nolock) where AliasName = '{0}'", endpointName);

                using (SqlConnection connection = new SqlConnection("Context Connection = true"))
                {
                    connection.Open();
                    
                    SqlCommand command = new SqlCommand(sql, connection);
                    var dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            endpoint = new RabbitEndpoint(dr);
                            break;
                        }
                    }
                }

                //SqlContext.Pipe.Send(endpoint.ToString());

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return endpoint;
        }

    }
}
