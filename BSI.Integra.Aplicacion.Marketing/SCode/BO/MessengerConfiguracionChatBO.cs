using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class MessengerConfiguracionChatBO : BaseEntity
    {
        public string NombreConfiguracion { get; set; }
        public string TextoOffline { get; set; }
        public string TextoSatisfaccionOffline { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
