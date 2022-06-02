using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MessengerUsuarioLogBO : BaseBO
    {
        public int? IdMessengerUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
    }
}
