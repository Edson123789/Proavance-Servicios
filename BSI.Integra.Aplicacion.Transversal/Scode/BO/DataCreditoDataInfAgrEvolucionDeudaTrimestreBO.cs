using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrEvolucionDeudaTrimestreBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Cupototal { get; set; }
        public decimal? Saldo { get; set; }
        public string PorcentajeUso { get; set; }
        public decimal? Score { get; set; }
        public string Calificacion { get; set; }
        public string AperturaCuentas { get; set; }
        public string CierreCuentas { get; set; }
        public int? TotalAbiertas { get; set; }
        public int? TotalCerradas { get; set; }
        public string MoraMaxima { get; set; }
        public int? MesesMoraMaxima { get; set; }
    }
}
