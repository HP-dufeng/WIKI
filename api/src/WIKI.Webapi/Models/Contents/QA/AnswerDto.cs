using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WIKI.Core.Entities;
using WIKI.Mapper;

namespace WIKI.WebApi.Models
{
    public class AnswerDto : BaseDto
    {
        [Required]
        public virtual string Text { get; set; }

        public virtual long QuestionId { get; set; }

        public ExpandPropertyDto ExpandProperty { get; set; }
    }

    public static class AnswerDtoMapper
    {
        public static AnswerDto MapToDto(this Answer entity)
        {
            return AutoMapperHelper<AnswerDto, Answer>.Mapper.Map<AnswerDto>(entity);
        }
    }
}