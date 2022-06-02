using System;
using BSI.Integra.Aplicacion.Base.BO;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class ReporteFlujoBO : BaseBO
    {
        public string idMatriculaCabecera { get; set; }
        public string idCoordAcademico { get; set; }
        public string coordinador_Acad { get; set; }
        public string idPespecifico { get; set; }
        public string programa { get; set; }
        public string codigo_Alumno { get; set; }
        public string alumno { get; set; }
        public DateTime fecha_cuota { get; set; }
        public decimal monto_cuota { get; set; }
        public DateTime fecha_pago { get; set; }
        public decimal pago { get; set; }
        public decimal saldo_pendiente { get; set; }
        public decimal mora { get; set; }
        public string nro_cuota { get; set; }
        public string moneda { get; set; }
        public decimal totalUSD { get; set; }
        public decimal realUSD { get; set; }
        public decimal penUSD { get; set; }
    }
}
