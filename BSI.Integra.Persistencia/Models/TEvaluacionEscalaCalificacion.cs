using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEvaluacionEscalaCalificacion
    {
        public int Id { get; set; }
        public int IdModalidadCurso { get; set; }
        public string CodigoCiudad { get; set; }
        public decimal EscalaCalificacion { get; set; }
        public decimal NotaAprobatoria { get; set; }
        public int RedondeoDecimales { get; set; }
        public string EscalaTexto { get; set; }
        public string NotaAprobatoriaTexto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
