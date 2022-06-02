using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BeneficioSolicitadoReporteDTO
    {
        public int Id { get; set; }
        public string Alumno { get; set; }
        public string CodigoMatricula { get; set; }
        public string Beneficio { get; set; }
        public string Programa { get; set; }
        public string CentroCosto { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Coordinador { get; set; }
        public string EstadoSolicitud { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string UsuarioEntregoBeneficio { get; set; }
        //public List<DatoAdicionalPWDTO> DatosAdicionales { get; set; }
    }
}
