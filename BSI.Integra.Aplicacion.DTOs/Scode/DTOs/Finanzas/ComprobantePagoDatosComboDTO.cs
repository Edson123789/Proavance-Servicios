using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComprobantePagoDatosComboDTO
    {
        public int Id { get; set; }
        public int? IdProveedor { get; set; }
        public string NumeroComprobante { get; set; }
        public decimal MontoNeto { get; set; }

    }
}
