using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoCursoComplementario
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int IdTipoCompetenciaTecnica { get; set; }
        public int IdCompetenciaTecnica { get; set; }
        public int? IdNivelCompetenciaTecnica { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TNivelCompetenciaTecnica IdNivelCompetenciaTecnicaNavigation { get; set; }
        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; }
        public virtual TTipoCompetenciaTecnica IdTipoCompetenciaTecnicaNavigation { get; set; }
    }
}
