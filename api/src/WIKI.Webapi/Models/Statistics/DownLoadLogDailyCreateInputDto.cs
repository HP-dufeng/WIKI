using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIKI.Core.Entities;
using WIKI.Mapper;

namespace WIKI.WebApi.Models
{
    public class DownLoadLogDailyCreateInputDto
    {
        public long AttachmentId { get; set; }
    }

    public static class DownLoadLogDailyCreateInputDtoMapper
    {
        public static DownLoadLogDaily MapToEntity(this DownLoadLogDailyCreateInputDto dto)
        {
            return AutoMapperHelper<DownLoadLogDaily, DownLoadLogDailyCreateInputDto>.Mapper.Map<DownLoadLogDaily>(dto);
        }
    }
}