using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Data;
using WIKI.Data.Auditing;

namespace WIKI.Core.Entities
{
    public class Document : BaseContent
    {
        public Document()
        {
            ContentNo = Consts.DocumentPrefix + DateTime.Now.ToString("yyyyMMddHHmmss");
            ContentType = ContentTypeEnum.Document.ToString();
        }

        [MaxLength(200)]
        public virtual string Description { get; set; }

        public virtual List<DocumentAttachment> DocumentAttachmentList { get; set; }
    }

}
