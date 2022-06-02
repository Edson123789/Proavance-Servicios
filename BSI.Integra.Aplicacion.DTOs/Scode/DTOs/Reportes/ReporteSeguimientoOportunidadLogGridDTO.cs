using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteSeguimientoOportunidadLogGridDTO
    {
        public string FaseInicio { get; set; }
        public string FaseDestino { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public string EstadoFase { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Estado { get; set; }
        public string TiempoDuracion { get; set; }
        public string TiempoDuracion3CX { get; set; }
        public List<LlamadaIntegraDTO> LlamadaIntegra { get; set; }
        public List<LlamadaIntegraDTO> LlamadaTresCX { get; set; }
        public string NombreActividad { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public int TotalEjecutadas { get; set; }
        public int TotalNoEjecutadas { get; set; }
        public int TotalAsignacionManual { get; set; }
        public int? IdFaseOportunidad { get; set; }
    }
}
