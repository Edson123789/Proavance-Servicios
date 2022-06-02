using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DataOportunidadRN2DTO
    {
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public int IdFaseOportunidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimaFechaProgramada { get; set; }

    }
}
