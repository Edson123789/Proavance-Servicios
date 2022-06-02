using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReportePagoFiltroDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdFormaPago { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdModalidad { get; set; }
        public int? IdCambio { get; set; }
    }
}
