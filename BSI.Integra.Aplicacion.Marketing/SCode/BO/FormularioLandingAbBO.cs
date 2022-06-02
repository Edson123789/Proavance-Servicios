using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FormularioLandingAbBO : BaseBO
    {
        public int IdTesteoAb { get; set; }
        public string TextoFormulario { get; set; }
        public string NombrePrograma { get; set; }
        public string Descripcion { get; set; }
        public Guid? IdMigracion { get; set; }
        public List<SeccionFormularioAbBO> ListaSeccionFormularioAbBO { get; set; }

        public FormularioLandingAbBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
