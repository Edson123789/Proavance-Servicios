using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaCabeceraDatosCertificadoMensajes
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonalRemitente { get; set; }
        public int IdPersonalReceptor { get; set; }
        public string Mensaje { get; set; }
        public string ValorAntiguo { get; set; }
        public string ValorNuevo { get; set; }
        public bool EstadoMensaje { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
