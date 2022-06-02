using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? CupoTotal { get; set; }
        public decimal? Saldo { get; set; }
        public string Porcentaje { get; set; }
        public decimal? Score { get; set; }
        public int? Calificacion { get; set; }
        public decimal? AperturaCuentas { get; set; }
        public decimal? CierreCuentas { get; set; }
        public string TotalAbiertas { get; set; }
        public string TotalCerradas { get; set; }
        public decimal? MoraMaxima { get; set; }
    }
}
