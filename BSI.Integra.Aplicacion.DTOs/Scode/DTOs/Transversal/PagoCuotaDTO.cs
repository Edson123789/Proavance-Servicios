using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PagoCuotaDTO
    {
        public int numeroCuota { get; set; }
        public string cuotaDescripcion { get; set; }
        public double? montoCuota { get; set; }
        public float montoCuotaDescuento { get; set; }
        public bool ispagado { get; set; }
        public bool es_matricula { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechapago { get; set; }
        public string SimboloMoneda { get; set; }
    }
}
