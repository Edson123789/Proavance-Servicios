using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDocumentacionPersonal
    {
        public int Id { get; set; }
        public int IdTipoDocumentacionPersonal { get; set; }
        public int IdPersonal { get; set; }
        public string UrlDocumento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TTipoDocumentacionPersonal IdTipoDocumentacionPersonalNavigation { get; set; }
    }
}
