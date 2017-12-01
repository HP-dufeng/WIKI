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
    [Table("Download_Log_Daily")]
    public class DownLoadLogDaily : Entity<long>
    {
        public virtual long AttachmentId { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public DownLoadLogDaily()
        {
            CreatedTime = DateTime.Now;
        }
    }
}
