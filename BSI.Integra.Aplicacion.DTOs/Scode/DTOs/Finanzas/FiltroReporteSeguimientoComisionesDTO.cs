using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroReporteSeguimientoComisionesDTO
    {
        public string ListaAsesores { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int IdEstadoComision { get; set; }
        public int IdSubEstadoComision { get; set; }

    }
}
