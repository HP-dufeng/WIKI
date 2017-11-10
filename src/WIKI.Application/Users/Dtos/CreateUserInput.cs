using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Users.Dtos
{
    public class CreateUserInput
    {
        [Required]
        public Guid WUCCUserId { get; set; }
    }
}
