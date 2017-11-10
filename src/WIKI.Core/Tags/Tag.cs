using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Tags
{
    public class Tag : AuditedEntity
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string ContentType { get; set; }
    }
}
