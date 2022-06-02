using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class FurPagoBO : BaseBO
    {
        public int? IdFur { get; set; }
        public int? IdComprobantePago { get; set; }
        public int NumeroPago { get; set; }
        public int IdMoneda { get; set; }
        public string NumeroRecibo { get; set; }
        public int IdFormaPago { get; set; }
        public DateTime FechaCobroBanco { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalMonedaDolares { get; set; }
        public int? IdMigracion { get; set; }
        public int IdCuentaCorriente { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }
    }
}
