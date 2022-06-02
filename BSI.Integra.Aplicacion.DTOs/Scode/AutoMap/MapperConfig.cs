using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace BSI.Integra.Aplicacion.DTOs.Scode.AutoMap
{
    public static class MapperConfig
    {
        public static void RegistarMapp()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<CuotaDTO, TEMP_TotalCuotas>()
                //    .ForMember(f => f.cuota, d => d.MapFrom(s => s.Monto))
                //    .ForMember(f => f.fecha_pago, d => d.MapFrom(s => s.FechaPago))
                //    .ReverseMap();
            });
        }
    }
}
