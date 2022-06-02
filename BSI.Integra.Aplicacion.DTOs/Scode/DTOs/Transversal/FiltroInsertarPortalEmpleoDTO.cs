using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroInsertarPortalEmpleoDTO
    {
        public PortalEmpleoDTO Objeto { get; set; }
        public List<int> Paises { get; set; }
    }
}
