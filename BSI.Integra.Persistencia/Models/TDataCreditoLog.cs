using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoLog
    {
        public int Id { get; set; }
        public string NumeroDocumento { get; set; }
        public string PrimerApellido { get; set; }
        public int? TipoIdentificacion { get; set; }
        public string RespuestaXml { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
