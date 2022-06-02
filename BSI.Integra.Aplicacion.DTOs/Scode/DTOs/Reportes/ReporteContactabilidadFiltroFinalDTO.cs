using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadFiltroFinalDTO
    {
        public string Asesores { get; set; }
        public string AsesoresComparativo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Tipo { get; set; }
    }
}
