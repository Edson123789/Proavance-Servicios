using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FormularioLandingPageCombosDTO
    {
        public List<PGeneralFiltroDTO> PGenerales { get; set; }
        public List<PlantillaLandingPageFiltroDTO> PlantillasLandingPage { get; set; }
    }
}
