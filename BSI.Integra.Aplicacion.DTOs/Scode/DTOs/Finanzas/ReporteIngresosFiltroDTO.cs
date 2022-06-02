using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteIngresosFiltroDTO
    {
        public DateTime? FechaInicioFiltro { get; set; }
        public DateTime? FechaFinFiltro { get; set; }
        public bool SeleccionoPeriodo { get; set; }
        public int? IdPeriodo { get; set; }
    }
}
