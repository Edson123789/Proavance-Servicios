using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAutoevaluacionPorCursoMoodle
    {
        public int Id { get; set; }
        public long? IdCategoria { get; set; }
        public long? IdCurso { get; set; }
        public string NombreCurso { get; set; }
        public long? IdEvaluacion { get; set; }
        public string Evaluacion { get; set; }
        public long? Orden { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? IdCursoModulo { get; set; }
        public int? IdSeccion { get; set; }
        public string NombreSeccion { get; set; }
        public bool? Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
