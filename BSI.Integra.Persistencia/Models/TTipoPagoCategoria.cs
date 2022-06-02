using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoPagoCategoria
    {
        public int Id { get; set; }
        public int IdCategoriaPrograma { get; set; }
        public int IdTipoPago { get; set; }
        public int IdModoPago { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TCategoriaPrograma IdCategoriaProgramaNavigation { get; set; }
        public virtual TModoPago IdModoPagoNavigation { get; set; }
        public virtual TTipoPago IdTipoPagoNavigation { get; set; }
    }
}
