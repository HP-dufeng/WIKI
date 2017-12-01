using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Data;

namespace WIKI.Core.Entities
{
    public class Account: Entity<long>
    {
        [Required]
        //[Index(IsUnique = true)]
        public Guid WUCC_UserId { get; set; }

        [Required]
        //[Index(IsUnique = true)]
        [MaxLength(20)]
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }
    }
}
