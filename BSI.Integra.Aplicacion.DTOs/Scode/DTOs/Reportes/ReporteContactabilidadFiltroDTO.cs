using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadFiltroDTO
    {
        public List<int> Asesores { get; set; }
        public List<int> AsesoresComparativos { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Tipo { get; set; }
    }
}
