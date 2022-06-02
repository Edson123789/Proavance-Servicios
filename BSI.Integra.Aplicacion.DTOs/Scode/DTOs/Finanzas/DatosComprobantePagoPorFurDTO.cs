using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosComprobantePagoPorFurDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public decimal MontoFur { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }
        public int? IdComprobantePago { get; set; }

    }
}
