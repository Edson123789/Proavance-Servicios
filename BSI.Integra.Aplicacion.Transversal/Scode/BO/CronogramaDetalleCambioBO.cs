using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class CronogramaDetalleCambioBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdCronogramaCabeceraCambio { get; set; }
        public int NroCuota { get; set; }
        public int NroSubcuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public decimal TipoCambio { get; set; }
        public string Moneda { get; set; }
        public int Version { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
