using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Classes
{
    public class AutoMapConverter<TSourceObj, TDestinationObj> : IAutoMapConverter<TSourceObj, TDestinationObj>
        where TSourceObj : class
        where TDestinationObj : class
    {
        private AutoMapper.IMapper mapper;

        public AutoMapConverter()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSourceObj, TDestinationObj>();
                
            });
            mapper = config.CreateMapper();
        }

        public TDestinationObj ConvertObject(TSourceObj srcObj)
        {
            return mapper.Map<TSourceObj, TDestinationObj>(srcObj);
        }

        public List<TDestinationObj> ConvertObjectCollection(IEnumerable<TSourceObj> srcObjList)
        {
            if (srcObjList == null) return null;
            var destList = srcObjList.Select(item => this.ConvertObject(item));
            return destList.ToList();
        }
    }
}
