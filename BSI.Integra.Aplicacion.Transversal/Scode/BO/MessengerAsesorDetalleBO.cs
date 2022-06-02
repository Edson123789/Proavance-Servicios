using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MessengerAsesorDetalleBO : BaseBO
    {
        public int IdMessengerAsesor { get; set; }
        public int IdPersonal { get; set; }
        public int IdAreaCapacitacionFacebook { get; set; }
        public Guid? IdMigracion { get; set; }

        public MessengerAsesorDetalleBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
