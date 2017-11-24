using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Data.Auditing;

namespace WIKI.Core.Entities
{
    public class Answer : AuditedEntity<long>
    {

        [Required]
        public virtual string Text { get; set; }

        public virtual long QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}
