using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WIKI.Core.Entities;
using WIKI.Mapper;

namespace WIKI.WebApi.Models
{
    public class DocumentUpdateInputDto
    {
        [Required]
        [MaxLength(60)]
        public virtual string Title { get; set; }

        [Required]
        public virtual bool Sticky { get; set; }

        [Required]
        public virtual bool Important { get; set; }

        [MaxLength(200)]
        public virtual string Description { get; set; }

        public List<string> Tags { get; set; }

        public List<DocumentAttachmentUpdateInputDto> DocumentAttachments { get; set; }
    }

    public class DocumentAttachmentUpdateInputDto
    {
        [StringLength(100)]
        public virtual string FileName { get; set; }

        [StringLength(50)]
        public virtual string DisplayName { get; set; }
    }

    public static class DocumentAttachmentUpdateInputDtoMapper
    {
        public static DocumentAttachment MapToEntity(this DocumentAttachmentUpdateInputDto dto)
        {
            return AutoMapperHelper<DocumentAttachment, DocumentAttachmentUpdateInputDto>.Mapper.Map<DocumentAttachment>(dto);
        }
    }
}