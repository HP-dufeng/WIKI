using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WIKI.Core.Entities;
using WIKI.Mapper;

namespace WIKI.WebApi.Models
{
    public class DocumentAttachmentDto:BaseDto
    {
        [StringLength(100)]
        public virtual string FileName { get; set; }

        [StringLength(50)]
        public virtual string DisplayName { get; set; }

        public long DownLoadCount { get; set; }
    }

    public static class DocumentAttachmentDtoMapper
    {
        public static DocumentAttachmentDto MapToDto(this DocumentAttachment entity)
        {
            return AutoMapperHelper<DocumentAttachmentDto, DocumentAttachment>.Mapper.Map<DocumentAttachmentDto>(entity);
        }
    }
}