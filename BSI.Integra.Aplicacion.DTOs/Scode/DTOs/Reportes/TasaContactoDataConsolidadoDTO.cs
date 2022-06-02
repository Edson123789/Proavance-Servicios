using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TasaContactoDataConsolidadoDTO
    {
        public List<TasaContactoDataDTO> Data { get; set; }
        public List<AsesorNombreFiltroDTO> Asesores { get; set; }
    }
}
