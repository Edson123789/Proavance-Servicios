using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TGastoFinancieroCronograma
    {
        public TGastoFinancieroCronograma()
        {
            TGastoFinancieroCronogramaDetalle = new HashSet<TGastoFinancieroCronogramaDetalle>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdMoneda { get; set; }
        public decimal CapitalTotal { get; set; }
        public decimal InteresTotal { get; set; }
        public DateTime FechaInicio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TGastoFinancieroCronogramaDetalle> TGastoFinancieroCronogramaDetalle { get; set; }
    }
}
