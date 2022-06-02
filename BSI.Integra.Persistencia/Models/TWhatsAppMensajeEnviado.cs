using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppMensajeEnviado
    {
        public int Id { get; set; }
        public string WaTo { get; set; }
        public string WaId { get; set; }
        public string WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string WaRecipientType { get; set; }
        public string WaBody { get; set; }
        public string WaFile { get; set; }
        public string WaFileName { get; set; }
        public string WaMimeType { get; set; }
        public string WaSha256 { get; set; }
        public string WaLink { get; set; }
        public string WaCaption { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }

        public virtual TPais IdPaisNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
    }
}
