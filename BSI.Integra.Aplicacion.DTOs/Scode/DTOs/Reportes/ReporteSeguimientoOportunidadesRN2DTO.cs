using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteSeguimientoOportunidadesRN2DTO
    {
        public int IdOportunidadRN2 { get; set; }
        public string NombreAlumno { get; set; }
        public string EstadoOportunidad { get; set; }
        public DateTime FechaProgramacionActual { get; set; }
        public DateTime? FechaProgramacionNueva { get; set; }
        public string FaseOportunidadRn2Actual { get; set; }
        public string FaseOportunidadRn2 { get; set; }
        public string FaseOportunidadClasificacionActual { get; set; }
        public string FaseOportunidadClasificacion { get; set; }
        public int Orden { get; set; }
        public string NombreCentroCostoRN2 { get; set; }
        public string NombreCentroCostoClasificacion { get; set; }
        public DateTime? FechaCreacionRN2 { get; set; }
        public DateTime? FechasCreacionClasificacion { get; set; }
        public int? IdOportunidadClasificacion { get; set; }
        public int IdHistoricoOportunidadRn2 { get; set; }
    }
}
