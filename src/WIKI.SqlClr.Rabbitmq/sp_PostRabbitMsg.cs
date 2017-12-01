using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using RabbitMQ.Client;
using System.Text;
using WIKI.SqlClr.Rabbitmq;

public partial class StoredProcedures
{


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void sp_PostRabbitMsg(string endpointName, string msg)
    {
        //step 0: read rabbit config
        var endpoint = Database.GetEndpointConfig(endpointName);

        if(endpoint == null)
        {
            SqlContext.Pipe.Send(string.Format("Endpoint: {0}, Not Found", endpointName));
            return;
        }

        if(!endpoint.IsEnabled)
        {
            SqlContext.Pipe.Send(string.Format("Endpoint: {0}, Disabled", endpointName));
            return;
        }

        //step 1: send msg to rabbit queue
        int i = 3;
        do
        {
            try
            {
                RabbitPublisher.SendMsg(endpoint, msg);
                break;
            }
            catch (Exception ex)
            {
                i--;

                if (i <= 0)
                    throw ex;
            }
        } while (i > 0);
        
        

    }


}
