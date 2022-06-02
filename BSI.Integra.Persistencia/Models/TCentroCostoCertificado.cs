using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCentroCostoCertificado
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdCertificadoBrochure { get; set; }
        public int? IdCertificadoPartnerComplemento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
