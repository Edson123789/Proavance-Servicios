using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFurPago
    {
        public int Id { get; set; }
        public int? IdFur { get; set; }
        public int? IdComprobantePago { get; set; }
        public int NumeroPago { get; set; }
        public int IdMoneda { get; set; }
        public string NumeroRecibo { get; set; }
        public int IdFormaPago { get; set; }
        public DateTime FechaCobroBanco { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalMonedaDolares { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdCuentaCorriente { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }

        public virtual TCuentaCorriente IdCuentaCorrienteNavigation { get; set; }
    }
}
