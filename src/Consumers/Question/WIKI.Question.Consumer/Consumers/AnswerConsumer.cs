using MassTransit;
using Nest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Question.Consumer;
using WIKI.Question.Consumer.Database;
using WIKI.Question.Consumer.ELK;

namespace WIKI.Question.Consumer
{
    public class AnswerConsumer : IConsumer<JToken>
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
                    response = IndexAccess.DeleteAnswer(message.ContentId.ToString());
                else
                {
                    var model = DataAccess.GetAnswerModel(message.ContentId);
                    if (model != null)
                        response = IndexAccess.UpdateAnswer(model);
                }

                if (response != null && response.IsValid)
                    DataAccess.SetAnswerMessageIndexed(message.Id);
            }
            catch(Exception ex)
            {
                Serilog.Log.Error("AnswerConsumer:Error");
                Serilog.Log.Error(ex.Message);
            }
            

            return Task.CompletedTask;
        }
    }
}
