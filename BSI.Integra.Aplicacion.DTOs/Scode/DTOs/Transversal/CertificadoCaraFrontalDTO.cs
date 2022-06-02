using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificadoCaraFrontalDTO
    {
        public int IdCertificadoDetalle { get; set; }
        public int IdCertificadoTipo { get; set; }
        public string NombreCertificadoTipo { get; set; }
        public int IdCertificadoTipoPrograma { get; set; }
        public string CodigoMatricula { get; set; }
        public string CodigoCertificado { get; set; }
        public bool TipoImpresion { get; set; }
        public string Ciudad { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public DateTime? FechaEmision { get; set; }
        public int? TamanioFuenteNombreAlumno { get; set; }
        public int? TamanioFuenteNombrePrograma { get; set; }
        public decimal? Nota { get; set; }
        public decimal? EscalaCalificacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public bool? EsAsistenciaPartner { get; set; }
        public CertificadoBrochureDTO Brochure { get; set; }
        public CertificadoPartnerComplementoDTO ComplementoPartner { get; set; }
        public CertificadoTipoProgramaDTO TipoPrograma { get; set; }
    }
}
