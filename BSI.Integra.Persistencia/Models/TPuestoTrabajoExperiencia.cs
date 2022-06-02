using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoExperiencia
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int IdExperiencia { get; set; }
        public int IdTipoExperiencia { get; set; }
        public int NumeroMinimo { get; set; }
        public string Periodo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TExperiencia IdExperienciaNavigation { get; set; }
        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; }
        public virtual TTipoExperiencia IdTipoExperienciaNavigation { get; set; }
    }
}
