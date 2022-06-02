using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMoodleCronogramaEvaluacion
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdCursoMoodle { get; set; }
        public int? IdEvaluacionMoodle { get; set; }
        public string NombreEvaluacion { get; set; }
        public DateTime FechaEstimada { get; set; }
        public int Orden { get; set; }
        public int Version { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
