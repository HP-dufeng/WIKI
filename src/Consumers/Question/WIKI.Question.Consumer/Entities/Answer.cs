using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Question.Consumer.Entities
{
    public class Answer
    {
        public long Id { get; set; }

        public long QuestionId { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual DateTime? UpdatedTime { get; set; }

        public virtual string UpdatedBy { get; set; }

      

        public virtual string Text { get; set; }

        public string EX_CreatedBy { get; set; }

        public string EX_UpdatedBy { get; set; }
        public string EX_CreatedByDept { get; set; }
        public string EX_UpdatedByDept { get; set; }
    }
}
