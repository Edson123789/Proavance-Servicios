using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPerfilPuestoTrabajo
    {
        public TPerfilPuestoTrabajo()
        {
            TPuestoTrabajoCaracteristicaPersonal = new HashSet<TPuestoTrabajoCaracteristicaPersonal>();
            TPuestoTrabajoCursoComplementario = new HashSet<TPuestoTrabajoCursoComplementario>();
            TPuestoTrabajoDependencia = new HashSet<TPuestoTrabajoDependencia>();
            TPuestoTrabajoExperiencia = new HashSet<TPuestoTrabajoExperiencia>();
            TPuestoTrabajoFormacionAcademica = new HashSet<TPuestoTrabajoFormacionAcademica>();
            TPuestoTrabajoFuncion = new HashSet<TPuestoTrabajoFuncion>();
            TPuestoTrabajoPuestoAcargo = new HashSet<TPuestoTrabajoPuestoAcargo>();
            TPuestoTrabajoPuntajeCalificacion = new HashSet<TPuestoTrabajoPuntajeCalificacion>();
            TPuestoTrabajoRelacion = new HashSet<TPuestoTrabajoRelacion>();
            TPuestoTrabajoRelacionInterna = new HashSet<TPuestoTrabajoRelacionInterna>();
            TPuestoTrabajoReporte = new HashSet<TPuestoTrabajoReporte>();
        }

        public int Id { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string Descripcion { get; set; }
        public string Objetivo { get; set; }
        public int Version { get; set; }
        public bool? EsActual { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPersonalSolicitud { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public int? IdPersonalAprobacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string Observacion { get; set; }
        public int? IdPerfilPuestoTrabajoEstadoSolicitud { get; set; }

        public virtual TPerfilPuestoTrabajoEstadoSolicitud IdPerfilPuestoTrabajoEstadoSolicitudNavigation { get; set; }
        public virtual ICollection<TPuestoTrabajoCaracteristicaPersonal> TPuestoTrabajoCaracteristicaPersonal { get; set; }
        public virtual ICollection<TPuestoTrabajoCursoComplementario> TPuestoTrabajoCursoComplementario { get; set; }
        public virtual ICollection<TPuestoTrabajoDependencia> TPuestoTrabajoDependencia { get; set; }
        public virtual ICollection<TPuestoTrabajoExperiencia> TPuestoTrabajoExperiencia { get; set; }
        public virtual ICollection<TPuestoTrabajoFormacionAcademica> TPuestoTrabajoFormacionAcademica { get; set; }
        public virtual ICollection<TPuestoTrabajoFuncion> TPuestoTrabajoFuncion { get; set; }
        public virtual ICollection<TPuestoTrabajoPuestoAcargo> TPuestoTrabajoPuestoAcargo { get; set; }
        public virtual ICollection<TPuestoTrabajoPuntajeCalificacion> TPuestoTrabajoPuntajeCalificacion { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacion> TPuestoTrabajoRelacion { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacionInterna> TPuestoTrabajoRelacionInterna { get; set; }
        public virtual ICollection<TPuestoTrabajoReporte> TPuestoTrabajoReporte { get; set; }
    }
}
