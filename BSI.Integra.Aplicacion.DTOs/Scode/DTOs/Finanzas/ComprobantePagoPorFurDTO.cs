using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComprobantePagoPorFurDTO
    {
        public int Id { get; set; }
        public int IdComprobantePago { get; set; }
        public int IdFur { get; set; }
        public decimal Monto { get; set; }

    }
    public class ComprobantePagoPorFurCodigoDTO {

        public int Id { get; set; }
        public string Codigo { get; set; }
        public decimal MontoFur { get; set; }
        public int IdComprobantePagoPorFur { get; set; }
        public int IdComprobantePago { get; set; }
    }
}
