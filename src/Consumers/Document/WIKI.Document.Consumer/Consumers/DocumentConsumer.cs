using MassTransit;
using Nest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Document.Consumer.Database;
using WIKI.Document.Consumer.ELK;

namespace WIKI.Document.Consumer
{
    public class DocumentConsumer : IConsumer<JToken>
    {
        public Task Consume(ConsumeContext<JToken> context)
        {
            try
            {
                var message = context.Message.ToObject<Message>();
                if (message == null)
                    return Task.CompletedTask;

                IResponse response = null;
                if (message.IsDelete)
                    response = IndexAccess.DeleteDocument(message.ContentId);
                else
                {
                    var model = DataAccess.GetDocumentModel(message.ContentId);
                    if (model != null)
                        response = IndexAccess.UpdateDocument(model);
                }

                if (response != null && response.IsValid)
                    DataAccess.SetDocumentMessageIndexed(message.Id);
                else
                    Serilog.Log.Error(response.DebugInformation);
            }
            catch(Exception ex)
            {
                Serilog.Log.Error("DocumentConsumer:Error");
                Serilog.Log.Error(ex.Message);
            }
    

            return Task.CompletedTask;
        }
    }

   
}
