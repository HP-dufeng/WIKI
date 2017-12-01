using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIKI.WebApi.Models
{
    public class BaseDto
    {
        public long Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual DateTime? UpdatedTime { get; set; }

        public virtual string UpdatedBy { get; set; }
    }
}