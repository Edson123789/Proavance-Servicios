using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoCaracteristicaPersonal
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int EdadMinima { get; set; }
        public int EdadMaxima { get; set; }
        public int IdSexo { get; set; }
        public int IdEstadoCivil { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TEstadoCivil IdEstadoCivilNavigation { get; set; }
        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; }
        public virtual TSexo IdSexoNavigation { get; set; }
    }
}
