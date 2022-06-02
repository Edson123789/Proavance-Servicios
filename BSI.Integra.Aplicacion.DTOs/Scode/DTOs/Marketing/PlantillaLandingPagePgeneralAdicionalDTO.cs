using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlantillaLandingPagePgeneralAdicionalDTO
    {
        public int Id { get; set; }
        public int IdPlantillaLandingPage { get; set; }
        public int? IdTitulo { get; set; }
        public string NombreTitulo { get; set; }
        public int? IdAdicionalProgramaGeneral { get; set; }
        public string ColorTitulo { get; set; }
        public string ColorDescripcion { get; set; }
    }
}
