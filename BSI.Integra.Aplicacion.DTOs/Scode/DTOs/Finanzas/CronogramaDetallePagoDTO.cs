using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CronogramaDetallePagoDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }// ver
        public int NroCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal Cuota { get; set; }
        public decimal Saldo { get; set; }
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }

    }
}
