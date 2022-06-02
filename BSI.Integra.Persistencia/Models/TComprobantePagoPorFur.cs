using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TComprobantePagoPorFur
    {
        public int Id { get; set; }
        public int IdComprobantePago { get; set; }
        public int IdFur { get; set; }
        public decimal Monto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TComprobantePago IdComprobantePagoNavigation { get; set; }
        public virtual TFur IdFurNavigation { get; set; }
    }
}
