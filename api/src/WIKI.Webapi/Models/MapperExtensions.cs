using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIKI.Core.Entities;
using WIKI.Mapper;

namespace WIKI.WebApi.Models
{
    public static class MapperExtensions
    {
        public static Content MapToContent(this Question entity)
        {
            return AutoMapperHelper<Content, Question>.Mapper.Map<Content>(entity);
        }

        public static Content MapToContent(this Document entity)
        {
            return AutoMapperHelper<Content, Document>.Mapper.Map<Content>(entity);
        }

    }
}