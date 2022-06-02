using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrResumenSaldoMesBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? SaldoTotalMora { get; set; }
        public decimal? SaldoTotal { get; set; }
    }
}
