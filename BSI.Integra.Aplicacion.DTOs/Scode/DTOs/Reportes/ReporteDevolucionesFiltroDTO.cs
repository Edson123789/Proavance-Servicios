using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteDevolucionesFiltroDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaInicioCronograma { get; set; }
        public DateTime? FechaFinCronograma { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdMatricula { get; set; }
        public List<int> IdEstadoPagoMatricula { get; set; }
    }
}
