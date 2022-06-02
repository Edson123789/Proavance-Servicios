using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MessengerAsesorBO : BaseBO
    {
        public int IdPersonal { get; set; }
        public int? ConteoClientesAsignados { get; set; }
        public Guid? IdMigracion { get; set; }


        public MessengerAsesorBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
