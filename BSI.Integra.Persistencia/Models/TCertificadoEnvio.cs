using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCertificadoEnvio
    {
        public int Id { get; set; }
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
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
