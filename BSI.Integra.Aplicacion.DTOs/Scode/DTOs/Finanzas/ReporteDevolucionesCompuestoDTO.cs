using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteDevolucionesCompuestoDTO
    {
        public List<ReporteDevolucionesDTO> ReporteDevoluciones { get; set; }
        public bool Cronograma { get; set; }
        public List<ReporteDevolucion> ReporteDevolucionAgrupado{ get; set; }

    }

    public class ReporteDevolucion
    {
        public string g { get; set; }
        public List<ReporteDevolucionesDTO> l { get; set; }
    }

}