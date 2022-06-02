using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteControlDocumentosFiltroDTO
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdCoordinador { get; set; }
        public int? IdAsesor { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdMatricula { get; set; }
        public string IdEstadoPagoMatricula { get; set; }
    }

    public class ReporteDocumentosFiltroDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Desglose { get; set; }
    }
}
