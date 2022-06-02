using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CronogramaFinalModificadoDTO
    {
        public string Id { get; set; }
        public bool Cancelado { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public string TipoCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public decimal Saldo { get; set; }
        public string Moneda { get; set; }
        public bool Enviado { get; set; }
    }
}
