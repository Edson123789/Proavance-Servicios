using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametroContenidoDatosDTO
    {
        public int Id { get; set; }
        public string NombreParametroSeo { get; set; }
        public int NumeroCaracteresParametrosSeo { get; set; }
        public string ContenidoParametroSeo { get; set; }
    }
}
