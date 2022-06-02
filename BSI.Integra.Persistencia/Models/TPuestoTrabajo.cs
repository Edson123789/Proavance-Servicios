using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajo
    {
        public TPuestoTrabajo()
        {
            TDatoContratoPersonal = new HashSet<TDatoContratoPersonal>();
            TGrupoComparacionProcesoSeleccion = new HashSet<TGrupoComparacionProcesoSeleccion>();
            TModuloSistemaPuestoTrabajo = new HashSet<TModuloSistemaPuestoTrabajo>();
            TPerfilPuestoTrabajoPersonalAprobacion = new HashSet<TPerfilPuestoTrabajoPersonalAprobacion>();
            TPersonalPuestoSedeHistorico = new HashSet<TPersonalPuestoSedeHistorico>();
            TPuestoTrabajoDependencia = new HashSet<TPuestoTrabajoDependencia>();
            TPuestoTrabajoPuestoAcargo = new HashSet<TPuestoTrabajoPuestoAcargo>();
            TPuestoTrabajoRelacionDetalleIdPuestoTrabajoDependenciaNavigation = new HashSet<TPuestoTrabajoRelacionDetalle>();
            TPuestoTrabajoRelacionDetalleIdPuestoTrabajoPuestoAcargoNavigation = new HashSet<TPuestoTrabajoRelacionDetalle>();
            TPuestoTrabajoRelacionExterna = new HashSet<TPuestoTrabajoRelacionExterna>();
            TPuestoTrabajoRemuneracion = new HashSet<TPuestoTrabajoRemuneracion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }

        public virtual TPersonalAreaTrabajo IdPersonalAreaTrabajoNavigation { get; set; }
        public virtual ICollection<TDatoContratoPersonal> TDatoContratoPersonal { get; set; }
        public virtual ICollection<TGrupoComparacionProcesoSeleccion> TGrupoComparacionProcesoSeleccion { get; set; }
        public virtual ICollection<TModuloSistemaPuestoTrabajo> TModuloSistemaPuestoTrabajo { get; set; }
        public virtual ICollection<TPerfilPuestoTrabajoPersonalAprobacion> TPerfilPuestoTrabajoPersonalAprobacion { get; set; }
        public virtual ICollection<TPersonalPuestoSedeHistorico> TPersonalPuestoSedeHistorico { get; set; }
        public virtual ICollection<TPuestoTrabajoDependencia> TPuestoTrabajoDependencia { get; set; }
        public virtual ICollection<TPuestoTrabajoPuestoAcargo> TPuestoTrabajoPuestoAcargo { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacionDetalle> TPuestoTrabajoRelacionDetalleIdPuestoTrabajoDependenciaNavigation { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacionDetalle> TPuestoTrabajoRelacionDetalleIdPuestoTrabajoPuestoAcargoNavigation { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacionExterna> TPuestoTrabajoRelacionExterna { get; set; }
        public virtual ICollection<TPuestoTrabajoRemuneracion> TPuestoTrabajoRemuneracion { get; set; }
    }
}
