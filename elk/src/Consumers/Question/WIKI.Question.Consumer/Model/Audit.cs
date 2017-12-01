﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIKI.Question.Consumer.Model
{
    public class Audit
    {
        public DateTime CreatedTime { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; }

        public string UpdatedBy { get; set; }
    }

    
}