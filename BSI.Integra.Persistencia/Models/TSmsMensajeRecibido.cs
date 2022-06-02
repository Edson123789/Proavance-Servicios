using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSmsMensajeRecibido
    {
        public int Id { get; set; }
        public string NumeroTelefono { get; set; }
        public string Puerto { get; set; }
        public string NombrePuerto { get; set; }
        public string Mensaje { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string Imsi { get; set; }
        public string EstadoMensaje { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
