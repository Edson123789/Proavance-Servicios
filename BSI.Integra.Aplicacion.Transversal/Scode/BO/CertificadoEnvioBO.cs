using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoEnvioBO : BaseBO
    {
        public int IdCertificadoBrochure { get; set; }
        public int IdCertificadoDetalle { get; set; }
        public int IdCertificadoFormaEntrega { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string CodigoSeguimiento { get; set; }
        public string Observacion { get; set; }
        public int? IdCertificadoFormaEntregaPartner { get; set; }
        public DateTime? FechaEnvioPartner { get; set; }
        public DateTime? FechaRecepcionPartner { get; set; }
        public string CodigoSeguimientoPartner { get; set; }
        public string ObservacionesPartner { get; set; }
        public int? IdMigracion { get; set; }
    }
}
