using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoAreaDTO
    {
        public AreaCapacitacionDTO AreaCapacitacion { get; set; }
        public List<ParametroContenidoDatosDTO> ListaParametro { get; set; }
        public string Usuario { get; set; }

    }
}
