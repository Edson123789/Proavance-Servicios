using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class CronogramaPagoDetalleBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal Cuota { get; set; }
        public decimal Saldo { get; set; }
        public bool Cancelado { get; set; }
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }
        public int? IdMigracion { get; set; }
    }
}
