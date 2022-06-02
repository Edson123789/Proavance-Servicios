using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDocumentoOportunidad
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdOportunidad { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string Comentario { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }
    }
}
