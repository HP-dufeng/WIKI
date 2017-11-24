using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIKI.Mapper
{
    public class AutoMapperHelper<TDistination, TSource>
    {
        #region Map
        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    _mapper = new MapperConfiguration(config =>
                        config.CreateMap<TSource, TDistination>()
                    ).CreateMapper();
                }
                return _mapper;
            }
        }
        #endregion
    }
}