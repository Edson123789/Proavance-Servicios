using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoComprobantePagoDTO
    {
        public ComprobantePagoInsercionDTO ComprobantePago { get; set; }
        public List<DatosComprobantePagoPorFurDTO> ComprobantePagoPorFur { get; set; }
        public string Usuario { get; set; }
        public int IdFur { get; set; }

    }
}
