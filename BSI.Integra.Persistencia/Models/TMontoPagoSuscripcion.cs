using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMontoPagoSuscripcion
    {
        public int Id { get; set; }
        public int IdMontoPago { get; set; }
        public int IdSuscripcionProgramaGeneral { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TMontoPago IdMontoPagoNavigation { get; set; }
    }
}
