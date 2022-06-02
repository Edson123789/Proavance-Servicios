using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TExamen
    {
        public TExamen()
        {
            TConfigurarExamenesEncuestasEstructura = new HashSet<TConfigurarExamenesEncuestasEstructura>();
            TExamenAsignadoEvaluador = new HashSet<TExamenAsignadoEvaluador>();
            TPuestoTrabajoPuntajeCalificacion = new HashSet<TPuestoTrabajoPuntajeCalificacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public bool? RequiereTiempo { get; set; }
        public int? DuracionMinutos { get; set; }
        public string Instrucciones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdExamenConfiguracionFormato { get; set; }
        public int? IdExamenComportamiento { get; set; }
        public int? IdExamenConfigurarResultado { get; set; }
        public int? IdCriterioEvaluacionProceso { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public bool? RequiereCentil { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public decimal? Factor { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? CantidadDiasAcceso { get; set; }

        public virtual ICollection<TConfigurarExamenesEncuestasEstructura> TConfigurarExamenesEncuestasEstructura { get; set; }
        public virtual ICollection<TExamenAsignadoEvaluador> TExamenAsignadoEvaluador { get; set; }
        public virtual ICollection<TPuestoTrabajoPuntajeCalificacion> TPuestoTrabajoPuntajeCalificacion { get; set; }
    }
}
