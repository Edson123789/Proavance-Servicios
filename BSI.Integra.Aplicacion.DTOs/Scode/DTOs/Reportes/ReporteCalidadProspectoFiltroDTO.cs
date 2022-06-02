using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCalidadProspectoFiltroDTO
    {
        public List<int> Coordinadores { get; set; }
        public List<int> Asesores { get; set; }
        public List<int> Grupos { get; set; }
        public DateTime FechaInicio  { get; set; }
        public DateTime FechaFin  { get; set; }
    }
}
