using FileCenter.MassTransit.Event.Event;
using MassTransit;
using Newtonsoft.Json;
using WIKI.MassTransit.Consumer.UploadEventConsumer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WIKI.MassTransit.Consumer.UploadEventConsumer
{
    public class UploadAttachmentEventConsumer : IConsumer<UploadAttachmentEvent>
    {
        public Task Consume(ConsumeContext<UploadAttachmentEvent> context)
        { 
            try
            {
                UploadData d = JsonConvert.DeserializeObject<UploadData>(context.Message.Data);
                Type type = Type.GetType(typeof(BaseUploadEventHandler).Namespace + ".Handlers." + d.EventType + "EventHandler");
                BaseUploadEventHandler handler = (BaseUploadEventHandler)Activator.CreateInstance(type);
                handler.Do(context.Message);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message.ToString());
                throw new Exception("事件处理失败:" + ex.Message.ToString());
            }

            return Task.CompletedTask;            
        }
    }
}
