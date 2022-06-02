using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class MessengerChatBO : BaseEntity
    {
        public int? IdMeseengerUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public string Mensaje { get; set; }
        public bool? Tipo { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
