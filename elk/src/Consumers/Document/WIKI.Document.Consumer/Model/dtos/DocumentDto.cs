using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using DapperExtensions.Mapper;
using AutoMapper;

namespace WIKI.Document.Consumer.Model
{
    public class DocumentDto
    {

        public long Id { get; set; }
        
        public string ContentNo { get; set; }

        public string ContentType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Sticky { get; set; }

        public bool Important { get; set; }

        public List<string> Tags { get; set; }

        public Audit Audit { get; set; }

        public ExpandProperty ExpandProperty { get; set; }

        public List<DocumentAttachmentDto> DocumentAttachment { get; set; }

        public DocumentDto()
        {
            Audit = new Audit();
            ExpandProperty = new ExpandProperty();
            DocumentAttachment = new List<Model.DocumentAttachmentDto>();
        }
    }

    public class DocumentAttachmentDto
    {
        public long Id { get; set; }
        public long DocumentId { get; set; }
        public string FileName { get; set; }
        public string DisplayName { get; set; }

        public Audit Audit { get; set; }

        public ExpandProperty ExpandProperty { get; set; }

        public DocumentAttachmentDto()
        {
            Audit = new Audit();
            ExpandProperty = new ExpandProperty();
        }
    }



    public static class DocumentDtoMapper
    {
        public static DocumentDto MapToDto(this Entities.Document entity)
        {
            var mapper = new MapperConfiguration(config =>
                config.CreateMap<Entities.Document, DocumentDto>()
                .AfterMap((s, d) => 
                {
                    d.ExpandProperty = new ExpandProperty
                    {
                        CreatedBy = s.EX_CreatedBy,
                        UpdatedBy = s.EX_UpdatedBy,
                        CreatedByDept = s.EX_CreatedByDept,
                        UpdatedByDept = s.EX_UpdatedByDept
                    };

                    d.Audit = new Audit
                    {
                        CreatedBy = s.CreatedBy,
                        CreatedTime = s.CreatedTime,
                        UpdatedBy = s.UpdatedBy,
                        UpdatedTime = s.UpdatedTime
                    };
                })
            ).CreateMapper();

            return mapper.Map<DocumentDto>(entity);
        }
    }


    public static class DocumentAttachmentDtoMapper
    {
        public static DocumentAttachmentDto MapToDto(this Entities.DocumentAttachment entity)
        {
            var mapper = new MapperConfiguration(config =>
                config.CreateMap<Entities.DocumentAttachment, DocumentAttachmentDto>()
                .AfterMap((s, d) =>
                {
                    d.ExpandProperty = new ExpandProperty
                    {
                        CreatedBy = s.EX_CreatedBy,
                        UpdatedBy = s.EX_UpdatedBy,
                        CreatedByDept = s.EX_CreatedByDept,
                        UpdatedByDept = s.EX_UpdatedByDept
                    };

                    d.Audit = new Audit
                    {
                        CreatedBy = s.CreatedBy,
                        CreatedTime = s.CreatedTime,
                        UpdatedBy = s.UpdatedBy,
                        UpdatedTime = s.UpdatedTime
                    };
                })
            ).CreateMapper();

            return mapper.Map<DocumentAttachmentDto>(entity);
        }
    }

}
