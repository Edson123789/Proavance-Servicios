using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoPaqueteDTO
    {
        public int Id { get; set; }
        public int? Paquete { get; set; }
    }
    public class PaqueteCentroCostoDTO
    {
        public int IdPaquete { get; set; }
        public string Paquete { get; set; }
        public int IdCentroCosto { get; set; }
    }
}
