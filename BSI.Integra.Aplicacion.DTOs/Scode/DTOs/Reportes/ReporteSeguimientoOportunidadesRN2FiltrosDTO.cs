using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteSeguimientoOportunidadesRN2FiltrosDTO
    {
        public List<int> Asesores { get; set; }
        public DateTime  FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool TipoFecha { get; set; }
    }
}
