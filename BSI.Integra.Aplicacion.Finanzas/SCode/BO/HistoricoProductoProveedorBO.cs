using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class HistoricoProductoProveedorBO : BaseBO
    {
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public decimal CostoMonedaOrigen { get; set; }
        public decimal CostoDolares { get; set; }
        public int IdMoneda { get; set; }
        public decimal Precio { get; set; }
        public decimal TipoCambio { get; set; }
        public int? IdCondicionPago { get; set; }
        public int IdCondicionTipoPago { get; set; }
        public int Version { get; set; }
        public string Observaciones { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
