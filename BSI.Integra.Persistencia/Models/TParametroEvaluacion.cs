using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TParametroEvaluacion
    {
        public int Id { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int IdEscalaCalificacion { get; set; }
        public string Nombre { get; set; }
        public int Ponderacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TCriterioEvaluacion IdCriterioEvaluacionNavigation { get; set; }
        public virtual TEscalaCalificacion IdEscalaCalificacionNavigation { get; set; }
    }
}
