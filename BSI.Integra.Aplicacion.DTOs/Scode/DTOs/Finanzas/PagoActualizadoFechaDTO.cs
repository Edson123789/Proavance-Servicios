using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PagoActualizadoFechaDTO
    {
        public int IdCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Usuario { get; set; }
    }
}
