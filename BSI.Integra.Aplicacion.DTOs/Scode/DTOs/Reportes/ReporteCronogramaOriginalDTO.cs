using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCronogramaOriginalDTO
    {
        public string EstadoMatricula { get; set; }
        public string Alumno { get; set; }
        public int matid { get; set; }
        public decimal cuota { get; set; }
        public int nrocuota { get; set; }
        public int nrosubcuota { get; set; }
        public string moneda { get; set; }
        public decimal cuotadolares { get; set; }
        public string CodigoMatricula { get; set; }
        public string PeriodoPorFechavencimiento { get; set; }
        public string Coordinadoraacademica { get; set; }
        public string Coordinadoracobranza { get; set; }
        public DateTime? fechavencimiento { get; set; }
      
    }
   
}
