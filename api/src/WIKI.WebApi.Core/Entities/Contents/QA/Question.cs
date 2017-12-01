using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Data.Auditing;

namespace WIKI.Core.Entities
{
    public class Question : BaseContent
    {
        public Question()
        {
            ContentNo = Consts.QuestionPrefix + DateTime.Now.ToString("yyyyMMddHHmmss");
            ContentType = ContentTypeEnum.Question.ToString();
        }

        [MaxLength(200)]
        public virtual string Text { get; set; }

        public virtual List<Answer> Answers { get; set; }

    }
}
