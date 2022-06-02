using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroReporteComisionesDTO
    {
        public string IdsAsesores { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int IdSubEstado { get; set; }

    }
}
