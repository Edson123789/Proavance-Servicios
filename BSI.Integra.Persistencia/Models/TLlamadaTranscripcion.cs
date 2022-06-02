using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TLlamadaTranscripcion
    {
        public int Id { get; set; }
        public int? IdLlamada { get; set; }
        public string RefLlamada { get; set; }
        public string RespuestaOriginal { get; set; }
        public string Transcripcion { get; set; }
        public string Clasificacion { get; set; }
        public string EstadoTranscripcion { get; set; }
        public string EstadoClasificacion { get; set; }
        public string ArchivoOriginal { get; set; }
        public bool? EsDuracionValida { get; set; }
        public int? IdDetalleClasificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
