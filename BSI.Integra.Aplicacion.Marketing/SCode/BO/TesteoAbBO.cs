using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class TesteoAbBO : BaseBO
    {
        public int IdFormularioLandingPage { get; set; }
        public int IdPlantillaLandingPage { get; set; }
        public string NombrePlantilla { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public int Porcentaje { get; set; }
        public Guid? IdMigracion { get; set; }
        public FormularioLandingAbBO FormularioLandingAb { get; set; }

        public TesteoAbBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
