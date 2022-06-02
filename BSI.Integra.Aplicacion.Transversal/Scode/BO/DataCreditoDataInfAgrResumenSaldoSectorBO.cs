using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrResumenSaldoSectorBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string Sector { get; set; }
        public decimal? Saldo { get; set; }
        public string Participacion { get; set; }
    }
}
