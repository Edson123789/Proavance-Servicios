using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoRelacionDetalle
    {
        public int Id { get; set; }
        public int IdPuestoTrabajoRelacion { get; set; }
        public int? IdPuestoTrabajoDependencia { get; set; }
        public int? IdPuestoTrabajoPuestoAcargo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPersonalAreaTrabajo IdPersonalAreaTrabajoNavigation { get; set; }
        public virtual TPuestoTrabajo IdPuestoTrabajoDependenciaNavigation { get; set; }
        public virtual TPuestoTrabajo IdPuestoTrabajoPuestoAcargoNavigation { get; set; }
        public virtual TPuestoTrabajoRelacion IdPuestoTrabajoRelacionNavigation { get; set; }
    }
}
