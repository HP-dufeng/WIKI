using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Data.Auditing;

namespace WIKI.Core.Entities
{
    public abstract class BaseContent: AuditedEntity<long>
    {
        [MaxLength(20)]
        public virtual string ContentNo { get; set; }

        [MaxLength(20)]
        public string ContentType { get; set; }

        [Required]
        [MaxLength(60)]
        public virtual string Title { get; set; }

        [Required]
        public virtual bool Sticky { get; set; }

        [Required]
        public virtual bool Important { get; set; }
    }
}
