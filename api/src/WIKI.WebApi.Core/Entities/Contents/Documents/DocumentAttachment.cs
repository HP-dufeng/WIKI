using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Data;
using WIKI.Data.Auditing;

namespace WIKI.Core.Entities
{
    public class DocumentAttachment : AuditedEntity<long>
    {
        [StringLength(100)]
        public virtual string FileName { get; set; }

        [StringLength(50)]
        public virtual string DisplayName { get; set; }

        public virtual long DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }
    }

}
