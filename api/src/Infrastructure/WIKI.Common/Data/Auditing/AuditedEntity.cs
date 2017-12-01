using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Data.Auditing
{
    [Serializable]
    public abstract class AuditedEntity<TKey> : Entity<TKey>
    {
        public virtual DateTime CreatedTime { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual DateTime? UpdatedTime { get; set; }

        public virtual string UpdatedBy { get; set; }

        protected AuditedEntity()
        {
            CreatedTime = DateTime.Now;
        }
    }

    [Serializable]
    public abstract class AuditedEntity : AuditedEntity<int>
    {

    }
}
