using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Knowledges.Documents;

namespace WIKI.Knowledges
{
    public class Document : AuditedEntity<long>
    {
        [Required]
        [MaxLength(60)]
        public virtual string Title { get; set; }

        public virtual string Tags { get; set; }

        [Required]
        public virtual bool Sticky { get; set; }

        [Required]
        public virtual bool Important { get; set; }

        [MaxLength(200)]
        public virtual string Description { get; set; }

        public virtual List<DocumentAttachment> DocumentAttachmentList { get; set; }
    }
}
