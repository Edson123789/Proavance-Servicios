using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoDetalleBO :BaseBO
    {
        public int? IdCertificadoBrochure { get; set; }
        public int IdCertificadoSolicitud { get; set; }
        public int IdCertificadoTipo { get; set; }
        public bool EsDiploma { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoCertificado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public decimal? Nota { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public decimal? EscalaCalificacion { get; set; }
        public DateTime? FechaEmision { get; set; }
        public int? TamanioFuenteNombreAlumno { get; set; }
        public int? TamanioFuenteNombrePrograma { get; set; }
        public string NombreArchivoFrontal { get; set; }
        public string NombreArchivoReverso { get; set; }
        public string NombreArchivoFrontalImpresion { get; set; }
        public string NombreArchivoReversoImpresion { get; set; }
        public string RutaArchivo { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        public string ContentType { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTime? FechaUltimoEnvioAlumno { get; set; }
        public int? IdCertificadoTipoPrograma { get; set; }
        public bool? EsAsistenciaPartner { get; set; }
        public bool AplicaPartner { get; set; }
        public int IdPespecifico { get; set; }
        public int? IdMigracion { get; set; }

        private CertificadoDetalleRepositorio _repCertificadoDetalle;
        private CertificadoBrochureRepositorio _repCertificadoBrochure;
        private CertificadoPartnerComplementoRepositorio _repCertificadoPartnerComplemento;
        private CertificadoTipoProgramaRepositorio _repCertificadoTipoPrograma;
        public CertificadoDetalleBO(integraDBContext _integraDBContext) 
        {
            _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            _repCertificadoBrochure = new CertificadoBrochureRepositorio(_integraDBContext);
            _repCertificadoPartnerComplemento = new CertificadoPartnerComplementoRepositorio(_integraDBContext);
            _repCertificadoTipoPrograma = new CertificadoTipoProgramaRepositorio(_integraDBContext);

        }
        public CertificadoDetalleBO() 
        {
        }

        public CertificadoCaraFrontalDTO CalcularCertificadoFrontal(int IdCertificadoDetalle, bool EsImpresion, int IdCertificadoBrochure)
        {
            var certificado = _repCertificadoDetalle.ObtenerDatosAlumnoCertificado(IdCertificadoDetalle);
            certificado.TipoImpresion = EsImpresion;
            certificado.FechaEmision = certificado.FechaEmision ?? DateTime.Now;
            certificado.Brochure = new CertificadoBrochureDTO();
            certificado.ComplementoPartner = new CertificadoPartnerComplementoDTO();
            certificado.TipoPrograma = new CertificadoTipoProgramaDTO();

            certificado.Brochure = _repCertificadoBrochure.FirstBy(w=> w.Id == IdCertificadoBrochure, y => new CertificadoBrochureDTO { NombreEnCertificado = y.NombreEnCertificado,TotalHoras = y.TotalHoras,Contenido = y.Contenido });

            certificado.ComplementoPartner = _repCertificadoPartnerComplemento.ObtenerPorCentroCosto(certificado.IdCentroCosto);

            certificado.TipoPrograma = _repCertificadoTipoPrograma.FirstBy(w=>w.Id == certificado.IdCertificadoTipoPrograma, y=> new CertificadoTipoProgramaDTO {AplicaFondoDiploma = y.AplicaFondoDiploma, AplicaNota = y.AplicaNota, AplicaSeOtorga = y.AplicaSeOtorga,NombreProgramaCertificado = y.NombreProgramaCertificado });

            certificado.Ciudad = CalcularCiudadCertificado(certificado.NombreCentroCosto);

            return certificado;
        }

        private string CalcularCiudadCertificado(string CentroCosto)
        {
            string ciudad = "Lima";

            if (CentroCosto.ToUpper().Contains(" ONLINE"))
                ciudad = "Lima";
            //presencial
            if (!CentroCosto.ToUpper().Contains(" ONLINE"))
            {
                if (CentroCosto.ToUpper().Contains($" {"Aqp"}"))
                    ciudad = "Arequipa";
                else if (CentroCosto.ToUpper().Contains($" {"Bogota"}"))
                    ciudad = "Bogotá";
                else if (CentroCosto.ToUpper().Contains("Santa Cruz"))
                    ciudad = "Santa Cruz";
                else if (CentroCosto.ToUpper().Contains($" {"Lima"}"))
                    ciudad = "Lima";
            }

            //excepciones
            if (CentroCosto.ToUpper().Contains("INHOUSE TABLEAU COLOMBIA_TELECOMUNICACIONES 2018 I AQP"))
                ciudad = "Bogotá";

            return ciudad;
        }

    }
}
