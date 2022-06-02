using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class AccionFormularioPorCategoriaOrigenBO : BaseBO
    {
        public int IdAccionFormulario { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public Guid IdMigracion { get; set; }

        public AccionFormularioPorCategoriaOrigenBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
