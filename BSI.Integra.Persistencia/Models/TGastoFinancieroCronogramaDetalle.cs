using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TGastoFinancieroCronogramaDetalle
    {
        public int Id { get; set; }
        public int IdGastoFinancieroCronograma { get; set; }
        public int NumeroCuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public DateTime FechaVencimientoCuota { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TGastoFinancieroCronograma IdGastoFinancieroCronogramaNavigation { get; set; }
    }
}
