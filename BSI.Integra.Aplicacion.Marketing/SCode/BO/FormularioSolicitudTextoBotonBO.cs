using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FormularioSolicitudTextoBotonBO : BaseBO
    {
        public string TextoBoton { get; set; }
        public string Descripcion { get; set; }
        public bool PorDefecto { get; set; }
        public Guid? IdMigracion { get; set; }

        public FormularioSolicitudTextoBotonBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
