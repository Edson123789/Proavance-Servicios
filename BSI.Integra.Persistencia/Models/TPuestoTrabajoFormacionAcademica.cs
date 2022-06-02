using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoFormacionAcademica
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public string IdTipoFormacion { get; set; }
        public string IdNivelEstudio { get; set; }
        public string IdAreaFormacion { get; set; }
        public string IdCentroEstudio { get; set; }
        public string IdGradoEstudio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; }
    }
}
