using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class PlantillaLandingPagePgeneralAdicionalBO : BaseBO
    {
        public int IdPlantillaLandingPage { get; set; }
        public int? IdTitulo { get; set; }
        public string NombreTitulo { get; set; }
        public int? IdAdicionalProgramaGeneral { get; set; }
        public string ColorTitulo { get; set; }
        public string ColorDescripcion { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
