using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrHistoricoSaldoTotalBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public int? TotalCuentas { get; set; }
        public int? CuentasConsideradas { get; set; }
        public decimal? Saldo { get; set; }
    }
}
