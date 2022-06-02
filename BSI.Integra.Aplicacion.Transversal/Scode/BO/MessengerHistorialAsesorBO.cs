using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MessengerHistorialAsesorBO : BaseBO
    {
        public int? IdMessengerAsesorDetalle { get; set; }
        public int? IdMessengerAsesor { get; set; }
        public DateTime Fecha { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdPersonal { get; set; }
        public MessengerHistorialAsesorBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
