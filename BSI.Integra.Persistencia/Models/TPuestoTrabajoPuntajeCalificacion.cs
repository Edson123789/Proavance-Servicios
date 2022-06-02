using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoPuntajeCalificacion
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int? IdExamen { get; set; }
        public bool CalificaPorCentil { get; set; }
        public decimal? PuntajeMinimo { get; set; }
        public int? IdProcesoSeleccionRango { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool EsCalificable { get; set; }

        public virtual TExamen IdExamenNavigation { get; set; }
        public virtual TExamenTest IdExamenTestNavigation { get; set; }
        public virtual TGrupoComponenteEvaluacion IdGrupoComponenteEvaluacionNavigation { get; set; }
        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; }
        public virtual TProcesoSeleccionRango IdProcesoSeleccionRangoNavigation { get; set; }
    }
}
