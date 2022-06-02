using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MessengerConfiguracionChatBO : BaseBO
    {
        public string NombreConfiguracion { get; set; }
        public string TextoOffline { get; set; }
        public string TextoSatisfaccionOffline { get; set; }
        public MessengerConfiguracionChatBO()
        {
        }
    }
}
