using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FormularioPlantillaBO : BaseBO
    {
        public string Nombre { get; set; }
        public int? IdFormularioSolicitud { get; set; }
        public int? IdFormularioLandingPage { get; set; }
        public int? IdMigracion { get; set; }
    }
}
