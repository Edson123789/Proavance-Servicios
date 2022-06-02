using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class NuevoAlumnoCongeladoBO: BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal Cuota { get; set; }
        public decimal Saldo { get; set; }
        public decimal Mora { get; set; }
        public decimal MontoPagado { get; set; }
        public bool Cancelado { get; set; }
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaCongelamiento { get; set; }
        public int IdPeriodo { get; set; }
    }
}
