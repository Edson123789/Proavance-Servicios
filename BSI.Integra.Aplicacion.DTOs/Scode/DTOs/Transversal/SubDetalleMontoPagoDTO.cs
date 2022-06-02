using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SubDetalleMontoPagoDTO
    {
        public List<int> MontoPagoPlataformas { get; set; }
        public List<int> MontoPagoSuscripciones { get; set; }
    }
}
