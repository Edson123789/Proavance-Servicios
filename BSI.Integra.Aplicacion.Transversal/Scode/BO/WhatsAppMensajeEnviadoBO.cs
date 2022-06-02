using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppMensajeEnviadoBO : BaseBO
    {
        public string WaTo { get; set; }
        public string WaId { get; set; }
        public string WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string WaRecipientType { get; set; }
        public string WaBody { get; set; }
        public string WaFile { get; set; }
        public string WaFileName { get; set; }
        public string WaMimeType { get; set; }
        public string WaSha256 { get; set; }
        public string WaLink { get; set; }
        public string WaCaption { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
