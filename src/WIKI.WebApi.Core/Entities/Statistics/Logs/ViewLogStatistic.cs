using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Data.Auditing;

namespace WIKI.Core.Entities
{
    [Table("View_Log_Statistic")]
    public class ViewLogStatistic : AuditedEntity<long>
    {
        public long ViewCount { get; set; }
    }
}
