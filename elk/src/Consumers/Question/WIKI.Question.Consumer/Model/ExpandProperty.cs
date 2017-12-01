using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Question.Consumer.Model
{
    public class ExpandProperty
    {
        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string CreatedByDept { get; set; }
        public string UpdatedByDept { get; set; }
    }
}