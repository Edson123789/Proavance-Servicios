using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrResumenSaldoBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public decimal? SaldoTotalEnMora { get; set; }
        public decimal? SaldoM30 { get; set; }
        public decimal? SaldoM60 { get; set; }
        public decimal? SaldoM90 { get; set; }
        public decimal? CuotaMensual { get; set; }
        public decimal? SaldoCreditoMasAlto { get; set; }
        public decimal? SaldoTotal { get; set; }
    }
}
