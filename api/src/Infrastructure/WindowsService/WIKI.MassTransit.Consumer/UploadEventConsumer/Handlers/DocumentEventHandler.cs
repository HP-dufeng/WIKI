using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileCenter.MassTransit.Event.Event;
using WIKI.MassTransit.Consumer.UploadEventConsumer.Data;
using Newtonsoft.Json;
using WIKI.Core.Entities;

namespace WIKI.MassTransit.Consumer.UploadEventConsumer.Handlers
{
    public class DocumentEventHandler : BaseUploadEventHandler
    {
        public override void Do(UploadAttachmentEvent events)
        {
            UploadData d = JsonConvert.DeserializeObject<UploadData>(events.Data);
            long _id = Convert.ToInt64(d.Id);
            Document entity = Db.Document.Find(_id);
            if (entity == null)
                throw new Exception("保存附件时查无此Document信息");

            DocumentAttachment _attachment = new DocumentAttachment();
            _attachment.DocumentId = entity.Id;
            _attachment.FileName = events.FileName;
            _attachment.DisplayName = events.DisplayName;

            Db.DocumentAttachment.Add(_attachment);
            Db.SaveChanges();
        }
    }
}
