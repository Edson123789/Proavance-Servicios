using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WhatsAppContactoDTO
    {
        public int IdPersonal { get; set; }
        public string[] contacts { get; set; }
        public string blocking { get; set; } //blocking
    }

    public class ValidarNumerosWhatsAppDTO
    {
        public string blocking { get; set; }
        public List<string> contacts { get; set; }
    }

    public class ValidarNumerosWhatsAppAsyncDTO
    {
        public List<string> contacts { get; set; }
    }

    // Creacion de objeto para su recepcion de la respuesta en JSON
    public class numerosValidos
    {
        public contacts[] contacts { get; set; }
        public meta meta { get; set; }
        public errors[] errors { get; set; }
    }

    public class contacts
    {
        public string input { get; set; }
        public string status { get; set; }
        public string wa_id { get; set; }
    }
}
