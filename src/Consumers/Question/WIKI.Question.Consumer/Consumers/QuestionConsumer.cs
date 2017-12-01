using MassTransit;
using Nest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Question.Consumer.Database;
using WIKI.Question.Consumer.ELK;

namespace WIKI.Question.Consumer
{
    public class QuestionConsumer : IConsumer<JToken>
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
                    response = IndexAccess.DeleteQuestion(message.ContentId);
                else
                {
                    var model = DataAccess.GetQuestionModel(message.ContentId);
                    if (model != null)
                        response = IndexAccess.UpdateQuestion(model);
                }

                if (response != null && response.IsValid)
                    DataAccess.SetQuestionMessageIndexed(message.Id);
                else
                    Serilog.Log.Error(response.DebugInformation);
            }
            catch(Exception ex)
            {
                Serilog.Log.Error("QuestionConsumer:Error");
                Serilog.Log.Error(ex.Message);
            }

            return Task.CompletedTask;
        }
    }

   
}
