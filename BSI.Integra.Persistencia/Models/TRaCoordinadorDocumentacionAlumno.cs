using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRaCoordinadorDocumentacionAlumno
    {
        public int Id { get; set; }
        public string CodigoAlumno { get; set; }
        public int IdCriterioDoc { get; set; }
        public bool? EnviadoContabilidad { get; set; }
        public byte Version { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string ContentType { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
