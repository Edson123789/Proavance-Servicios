using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TodoCertificadoEnvioDTO
    {
        public int? Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string CodigoCertificado { get; set; }
        public string NombreAlumno { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCertificadoTipo { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public int? IdCertificadoBrochure { get; set; }
        public int? NumeroSolicitud { get; set; }
        public string NombreCertificadoFormaEntrega { get; set; }
        public int? IdCertificadoDetalle { get; set; }
        public int? IdCertificadoFormaEntrega { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaEnvioDigital { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string Sistema { get; set; }
        public bool? AplicaPartner { get; set; }
        public string CodigoSeguimiento { get; set; }
        public string Observacion { get; set; }
        public int? IdCertificadoFormaEntrega_Partner { get; set; }
        public DateTime? FechaEnvio_Partner { get; set; }
        public DateTime? FechaRecepcion_Partner { get; set; }
        public string CodigoSeguimiento_Partner { get; set; }
        public string Observaciones_Partner { get; set; }
        public string NombreUsuario { get; set; }
    }
}
