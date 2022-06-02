using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MessengerChatBO : BaseBO
    {
        public Nullable<int> IdMeseengerUsuario { get; set; }
        public Nullable<int> IdPersonal { get; set; }
        public string Mensaje { get; set; }
        public Nullable<bool> Tipo { get; set; }
        public Guid? IdMigracion { get; set; }
        public string FacebookId { get; set; }
        public DateTime? FechaInteraccion { get; set; }
        public int? IdTipoMensajeMessenger { get; set; }
        public string UrlArchivoAdjunto { get; set; }
        public bool? Leido { get; set; }
        public DateTime? FechaLectura { get; set; }
        public int? IdFacebookAnuncio { get; set; }
        public MessengerChatBO() { }
    }
}
