using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostulanteComparacion
    {
        public int Id { get; set; }
        public int? IdPostulante { get; set; }
        public int? IdGrupoComparacionProcesoSeleccion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TGrupoComparacionProcesoSeleccion IdGrupoComparacionProcesoSeleccionNavigation { get; set; }
        public virtual TPostulante IdPostulanteNavigation { get; set; }
    }
}
