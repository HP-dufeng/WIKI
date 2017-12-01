using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.MassTransit.Consumer.UploadEventConsumer.Data
{

    public class UploadData
    {
        public string Id { get; set; }

        public string EventType { get; set; }
    }

}
