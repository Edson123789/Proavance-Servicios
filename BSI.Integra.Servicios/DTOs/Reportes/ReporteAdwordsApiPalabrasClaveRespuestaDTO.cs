using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.DTOs
{
    public class ReporteAdwordsApiPalabrasClaveRespuestaDTO
    {
        public string Pais { get; set; }
        public int IdPais { get; set; }
        public List<PalabraClaveVolumenDTO> Detalle { get; set; }

    }
}
