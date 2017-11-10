using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Knowledges.Documents
{
    public class DocumentAttachment : Entity
    {
        [StringLength(100)]
        public virtual string FileName { get; set; }

        [StringLength(50)]
        public virtual string DisplayName { get; set; }

        public virtual string FileExtension { get; set; }

        public virtual long DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }
    }
}
