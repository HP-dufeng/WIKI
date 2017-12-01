using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Data.Auditing;

namespace WIKI.Core.Entities
{
    [Table("view_Question")]
    public class QuestionView : AuditedEntity<long>
    {
        //public long Id { get; set; }

        public string ContentNo { get; set; }

        public string Title { get; set; }

        public virtual string Text { get; set; }

        public virtual bool Sticky { get; set; }

        public virtual bool Important { get; set; }

        //public DateTime CreatedTime { get; set; }

        //public string CreatedBy { get; set; }

        //public DateTime? UpdatedTime { get; set; }

        //public string UpdatedBy { get; set; }

        public string CreatedBy_FullName { get; set; }

        public string CreatedBy_Department { get; set; }

        public long? Answer_Id { get; set; }

        public string Answer_Text { get; set; }

        public DateTime? Answer_CreatedTime { get; set; }

        public DateTime? Answer_UpdatedTime { get; set; }

        public string Answer_UpdatedBy { get; set; }

        public string Answer_CreatedBy { get; set; }

        public string Answer_CreatedBy_FullName { get; set; }

        public string Answer_CreatedBy_Department { get; set; }

    }
}
