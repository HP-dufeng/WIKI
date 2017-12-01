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
    public class AnswerDto
    {
        public JoinField join_field { get; set; }

        // index id : "Id.QuestionId"
        public string Id { get; set; }

        public long AnswerId { get; set; }
        
        public string Text { get; set; }


        public Audit Audit { get; set; }

        public ExpandProperty ExpandProperty { get; set; }

        public AnswerDto()
        {
            join_field = new JoinField();
            Audit = new Audit();
            ExpandProperty = new ExpandProperty();
        }
    }

    public class JoinField
    {
        public string name { get; set; }
        public long parent { get; set; }
    }

    

    

    public static class AnswerDtoMapper
    {
        public static AnswerDto MapToDto(this Entities.Answer entity)
        {
            var mapper = new MapperConfiguration(config =>
                config.CreateMap<Entities.Answer, AnswerDto>()
                .ForMember(m => m.AnswerId, opt => opt.MapFrom(s => s.Id))
                .ForMember(m=>m.Id, opt =>opt.MapFrom(s=>s.Id + "." + s.QuestionId))
                .AfterMap((s, d) => 
                {

                    d.join_field = new JoinField
                    {
                        name = "answer",
                        parent = s.QuestionId
                    };

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

            return mapper.Map<AnswerDto>(entity);
        }
    }
}
