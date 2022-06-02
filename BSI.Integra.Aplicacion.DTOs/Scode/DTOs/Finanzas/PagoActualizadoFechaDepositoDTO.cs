using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PagoActualizadoFechaDepositoDTO
    {
        public int IdCuota { get; set; }
        public DateTime? FechaDeposito { get; set; }
        public string Usuario { get; set; }
    }
}
