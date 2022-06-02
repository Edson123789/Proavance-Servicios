using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class PortalEmpleoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Url { get; set; }
        public Guid? IdMigracion { get; set; }

        public PortalEmpleoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
