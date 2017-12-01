using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WIKI.Core.Entities;
using WIKI.Mapper;

namespace WIKI.WebApi.Models
{
    public class QuestionDto : BaseDto
    {

        [MaxLength(20)]
        public virtual string ContentNo { get; set; }

        [Required]
        [MaxLength(60)]
        public virtual string Title { get; set; }

        [MaxLength(200)]
        public virtual string Text { get; set; }

        [Required]
        public virtual bool Sticky { get; set; }

        [Required]
        public virtual bool Important { get; set; }

        public ExpandPropertyDto ExpandProperty { get; set; }

    }

    public static class QuestionDtoMapper
    {
        public static QuestionDto MapToDto(this Question entity)
        {
            return AutoMapperHelper<QuestionDto, Question>.Mapper.Map<QuestionDto>(entity);
        }
    }
}