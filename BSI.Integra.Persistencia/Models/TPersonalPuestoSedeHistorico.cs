using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPersonalPuestoSedeHistorico
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdSedeTrabajo { get; set; }
        public bool Actual { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TPuestoTrabajo IdPuestoTrabajoNavigation { get; set; }
        public virtual TSedeTrabajo IdSedeTrabajoNavigation { get; set; }
    }
}
