using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoRemuneracion
    {
        public TPuestoTrabajoRemuneracion()
        {
            TPuestoTrabajoRemuneracionDetalle = new HashSet<TPuestoTrabajoRemuneracionDetalle>();
        }

        public int Id { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdPais { get; set; }
        public int IdTableroComercialCategoriaAsesor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPuestoTrabajo IdPuestoTrabajoNavigation { get; set; }
        public virtual ICollection<TPuestoTrabajoRemuneracionDetalle> TPuestoTrabajoRemuneracionDetalle { get; set; }
    }
}
