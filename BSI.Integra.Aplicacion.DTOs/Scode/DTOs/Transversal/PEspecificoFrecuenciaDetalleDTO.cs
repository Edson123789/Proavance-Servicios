using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoFrecuenciaDetalleDTO
    {
        public byte? Dia { get; set; }
        public TimeSpan? HoraDia { get; set; }
        public decimal? Duracion { get; set; }
    }
}
