using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCentralLlamada
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string Central { get; set; }
        public string Anexo { get; set; }
        public string TelefonoDestino { get; set; }
        public string EstadoLlamada { get; set; }
        public int? Duracion { get; set; }
        public int? IdAlumno { get; set; }
        public string CodigoMatriculaCabecera { get; set; }
        public string Usuario { get; set; }
        public string Area { get; set; }
        public string Tipo { get; set; }
        public string RefLlamada { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
