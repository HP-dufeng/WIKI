using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIKI.WebApi.Models
{
    public class DocumentGetOutputDto
    {
        public DocumentDto Document { get; set; }

        public List<DocumentAttachmentDto> Attachments { get; set; }

        public List<string> Tags { get; set; }

        public long ViewCount { get; set; }
    }
}