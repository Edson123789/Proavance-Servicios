using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CronogramaPagoDetalleOriginalBO : BaseBO
    {
        public int? IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal Cuota { get; set; }
        public decimal Saldo { get; set; }
        public bool Cancelado { get; set; }
        public double? Monto { get; set; }
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }
        public decimal? TipocCambio { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
