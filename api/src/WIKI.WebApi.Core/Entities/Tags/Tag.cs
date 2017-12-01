using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Core.Entities
{
    public class Tag
    {
        [Key, Column(Order = 1)]
        public long ContentId { get; set; }

        [Required]
        [MaxLength(20)]
        [Key, Column(Order = 2)]
        public string Value { get; set; }

        [MaxLength(20)]
        public string ContentType { get; set; }

    }
}
