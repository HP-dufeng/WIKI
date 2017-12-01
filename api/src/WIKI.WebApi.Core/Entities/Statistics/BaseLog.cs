using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Data;

namespace WIKI.Core.Entities
{
    public abstract class BaseLog : Entity<long>
    {
        public virtual long ContentId { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public BaseLog()
        {
            CreatedTime = DateTime.Now;
        }
    }
}
