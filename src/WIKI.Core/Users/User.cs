using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Users
{
    public class User : Entity<long>, IHasCreationTime
    {
        public virtual Guid WUCCUserId { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public User()
        {
            CreationTime = DateTime.Now;
        }
    }
}
