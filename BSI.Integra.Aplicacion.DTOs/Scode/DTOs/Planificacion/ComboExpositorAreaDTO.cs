using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComboExpositorAreaDTO
    {
        public List<ExpositorFiltroDTO> ExpositorFiltro { get; set; }
        public List<FiltroDTO> AreaFiltro { get; set; }
    }
}
