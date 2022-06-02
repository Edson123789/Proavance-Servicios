using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoFuncion
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int NroOrden { get; set; }
        public string Nombre { get; set; }
        public int IdPersonalTipoFuncion { get; set; }
        public int IdFrecuenciaPuestoTrabajo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TFrecuenciaPuestoTrabajo IdFrecuenciaPuestoTrabajoNavigation { get; set; }
        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; }
        public virtual TPersonalTipoFuncion IdPersonalTipoFuncionNavigation { get; set; }
    }
}
