using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCriterioEvaluacion
    {
        public TCriterioEvaluacion()
        {
            TCriterioEvaluacionModalidadCurso = new HashSet<TCriterioEvaluacionModalidadCurso>();
            TCriterioEvaluacionTipoPersona = new HashSet<TCriterioEvaluacionTipoPersona>();
            TCriterioEvaluacionTipoPrograma = new HashSet<TCriterioEvaluacionTipoPrograma>();
            TEsquemaEvaluacionDetalle = new HashSet<TEsquemaEvaluacionDetalle>();
            TEsquemaEvaluacionPgeneralDetalle = new HashSet<TEsquemaEvaluacionPgeneralDetalle>();
            TParametroEvaluacion = new HashSet<TParametroEvaluacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCriterioEvaluacionCategoria { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdFormaCalculoEvaluacion { get; set; }
        public int? IdFormaCalificacionEvaluacion { get; set; }
        public int? IdFormaCalculoEvaluacionParametro { get; set; }

        public virtual TCriterioEvaluacionCategoria IdCriterioEvaluacionCategoriaNavigation { get; set; }
        public virtual TFormaCalificacionEvaluacion IdFormaCalificacionEvaluacionNavigation { get; set; }
        public virtual ICollection<TCriterioEvaluacionModalidadCurso> TCriterioEvaluacionModalidadCurso { get; set; }
        public virtual ICollection<TCriterioEvaluacionTipoPersona> TCriterioEvaluacionTipoPersona { get; set; }
        public virtual ICollection<TCriterioEvaluacionTipoPrograma> TCriterioEvaluacionTipoPrograma { get; set; }
        public virtual ICollection<TEsquemaEvaluacionDetalle> TEsquemaEvaluacionDetalle { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralDetalle> TEsquemaEvaluacionPgeneralDetalle { get; set; }
        public virtual ICollection<TParametroEvaluacion> TParametroEvaluacion { get; set; }
    }
}
