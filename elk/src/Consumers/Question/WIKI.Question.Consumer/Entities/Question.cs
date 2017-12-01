using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Question.Consumer.Entities
{
    public class Question
    {
        public long Id { get; set; }
        public virtual DateTime CreatedTime { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual DateTime? UpdatedTime { get; set; }

        public virtual string UpdatedBy { get; set; }

        public virtual string ContentNo { get; set; }

        public string ContentType { get; set; }

        public virtual string Title { get; set; }

        public virtual bool Sticky { get; set; }

        public virtual bool Important { get; set; }

        public virtual string Text { get; set; }

        public string EX_CreatedBy { get; set; }

        public string EX_UpdatedBy { get; set; }

        public string EX_CreatedByDept { get; set; }
        public string EX_UpdatedByDept { get; set; }
    }
}
