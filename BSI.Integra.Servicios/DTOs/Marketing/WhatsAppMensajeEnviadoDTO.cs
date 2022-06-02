using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.DTOs
{
    public class WhatsAppMensajeEnviadoDTO
    {
        public int Id { get; set; }
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

        //
        public int IdPersonal { get; set; }
        public int IdAlumno { get; set; }
        public string usuario { get; set; }
        public List<datosPlantillaWhatsApp> datosPlantillaWhatsApp { get; set; }
    }

	public class WhatsAppMensajeEnviadoPostulanteDTO
	{
		public int Id { get; set; }
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

		//
		public int IdPersonal { get; set; }
		public int IdPostulante { get; set; }
		public string usuario { get; set; }
		public List<datosPlantillaWhatsApp> datosPlantillaWhatsApp { get; set; }
	}

	public class datosPlantillaWhatsApp
    {
        public string codigo { get; set; }
        public string texto { get; set; }
    }

    #region Mensaje de Texto
    // Mensaje de Texto
    public class MensajeTextoEnvio
    {
        public string to { get; set; }
        public string type { get; set; }
        public string recipient_type { get; set; }
        public text text { get; set; }
    }

    public partial class text
    {
        public string body { get; set; }
    }
    #endregion

    #region Mensaje con plantillas de WhatsApp
    // Mensaje con plantillas de WhatsApp
    public class MensajePlantillaWhatsAppEnvio
    {
        public string to { get; set; }
        public string type { get; set; }
        public hsm hsm { get; set; }
        public template template { get; set; }
    }

    public partial class hsm
    {
        public string @namespace { get; set; }
        public string element_name { get; set; }
        public language language { get; set; }
        public List<localizable_params> localizable_params { get; set; }
    }

    public partial class language
    {
        public string policy { get; set; }
        public string code { get; set; }
    }

    public partial class localizable_params
    {
        public string @default { get; set; }
    }

    public class MensajePlantillaWhatsAppEnvioTemplate
    {
        public string to { get; set; }
        public string type { get; set; }
        public template template { get; set; }
    }

    public partial class template
    {
        public string @namespace { get; set; }
        public string name { get; set; }
        public language language { get; set; }
        public List<components> components { get; set; }
    }

    public partial class components
    {
        public string type { get; set; }
        public List<parameters> parameters { get; set; }
    }
    public partial class parameters
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    #endregion

    #region Mensaje con Imagen
    //Mensaje con imagen
    public class MensajeImagenEnvio
    {
        public string to { get; set; }
        public string type { get; set; }
        public string recipient_type { get; set; }
        public image image { get; set; }
    }

    public partial class image
    {
        public string caption { get; set; }
        public string link { get; set; }
    }
    #endregion

    #region Mensaje con documento
    //Mensaje con documento
    public class MensajeDocumentoEnvio
    {
        public string to { get; set; }
        public string type { get; set; }
        public string recipient_type { get; set; }
        public document document { get; set; }
    }

    public partial class document
    {
        public string caption { get; set; }
        public string link { get; set; }
        public string filename { get; set; }
    }
    #endregion

    #region MyRegion
    // Respuesta de mensaje enviado
    public partial class respuestaMensaje
    {
        public messages[] messages { get; set; }
        public meta meta { get; set; }
    }

    public partial class messages
    {
        public string id { get; set; }
    }
    #endregion


}
