using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDocumentoRecepcionado
    {
        public int Id { get; set; }
        public int IdPersonaTipoPersona { get; set; }
        public int IdDocumento { get; set; }
        public int IdPespecifico { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string MimeTypeArchivo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
