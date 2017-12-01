using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Question.Consumer
{
    public class Message
    {
        public string Id { get; set; }

        public long ContentId { get; set; }

        public bool IsDelete { get; set; }

    }
}
