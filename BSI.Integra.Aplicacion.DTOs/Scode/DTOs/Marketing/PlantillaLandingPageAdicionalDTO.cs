using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class PlantillaLandingPageAdicionalDTO
    {
        public PlantillaLandingPageDTO plantilla { get; set; }
        public List<PlantillaLandingPagePgeneralAdicionalDTO> listaAdicionales { get; set; }
        public string Usuario { get; set; }
    }
}
