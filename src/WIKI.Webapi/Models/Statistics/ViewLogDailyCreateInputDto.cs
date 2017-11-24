using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIKI.Core.Entities;
using WIKI.Mapper;

namespace WIKI.WebApi.Models
{
    public class ViewLogDailyCreateInputDto
    {
        public long ContentId { get; set; }
    }

    public static class ViewLogDailyCreateInputDtoMapper
    {
        public static ViewLogDaily MapToEntity(this ViewLogDailyCreateInputDto dto)
        {
            return AutoMapperHelper<ViewLogDaily, ViewLogDailyCreateInputDto>.Mapper.Map<ViewLogDaily>(dto);
        }
    }
}