using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIKI.WebApi.Models
{
    public class QuestionGetOutputDto
    {
        public QuestionDto Question { get; set; }

        public List<AnswerDto> Answers { get; set; }

        public List<string> Tags { get; set; }

        public long ViewCount { get; set; }
    }
}