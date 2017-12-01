using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using DapperExtensions.Mapper;
using AutoMapper;

namespace WIKI.Question.Consumer.Model
{
    public class QuestionDto
    {
        public string join_field { get; set; }

        public long Id { get; set; }
        
        public string ContentNo { get; set; }

        public string ContentType { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public bool Sticky { get; set; }

        public bool Important { get; set; }

        public List<string> Tags { get; set; }

        public Audit Audit { get; set; }

        public ExpandProperty ExpandProperty { get; set; }

        public QuestionDto()
        {
            join_field = "question";
            Audit = new Audit();
            ExpandProperty = new ExpandProperty();
        }
    }

    

    public static class QuestionDtoMapper
    {
        public static QuestionDto MapToDto(this Entities.Question entity)
        {
            var mapper = new MapperConfiguration(config =>
                config.CreateMap<Entities.Question, QuestionDto>()
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

            return mapper.Map<QuestionDto>(entity);
        }
    }
}
