using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InsertarWebHookDTO
    {
        public string PSID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Foto { get; set; }
        public string Mensaje { get; set; }
        public string Usuario { get; set; }
        public int? Asesor { get; set; }
        public bool EsPublicidad { get; set; }
        public string FacebookId { get; set; }
        public DateTime FechaInteraccion { get; set; }
        public string UrlArchivoAdjunto { get; set; }
        public int IdTipoMensajeMessenger { get; set; }
        public string RecipientId { get; set; }
    }
}
