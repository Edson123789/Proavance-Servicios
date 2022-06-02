using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TChatDetalleIntegra
    {
        public int Id { get; set; }
        public int? IdInteraccionChatIntegra { get; set; }
        public string NombreRemitente { get; set; }
        public string IdRemitente { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool? MensajeOfensivo { get; set; }
        public int? IdChatDetalleIntegraArchivo { get; set; }
    }
}
