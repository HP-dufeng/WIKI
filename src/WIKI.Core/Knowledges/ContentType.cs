using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Knowledges
{
    public class ContentType : Entity, IHasCreationTime
    {
        [Required]
        [MaxLength(20)]
        public virtual string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public virtual string Code { get; set; }

        [MaxLength(50)]
        public virtual string Description { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public ContentType()
        {
            CreationTime = DateTime.Now;
        }
    }
}
