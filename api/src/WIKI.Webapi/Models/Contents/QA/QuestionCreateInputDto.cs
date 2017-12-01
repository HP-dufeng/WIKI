using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WIKI.Core.Entities;

namespace WIKI.WebApi.Models
{
    public class QuestionCreateInputDto
    {
        [Required]
        [MaxLength(60)]
        public virtual string Title { get; set; }

        [MaxLength(200)]
        public virtual string Text { get; set; }

        [Required]
        public virtual bool Sticky { get; set; }

        [Required]
        public virtual bool Important { get; set; }

        public List<string> Tags { get; set; }

        public List<string> Answers { get; set; }
    }

    public static class QuestionCreateInputDtoMapper
    {
        public static Question MapToEntity(this QuestionCreateInputDto dto)
        {
            var mapper = new MapperConfiguration(config =>
                config.CreateMap<QuestionCreateInputDto, Question>().ForMember(d =>d.Answers, opt => opt.Ignore()).AfterMap((d, s) => { s.Answers = new List<Answer>(); })
            ).CreateMapper();

            return mapper.Map<Question>(dto);

        }
    }
}