using System;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ResumenCorreoGmailDTO
    {
        public int IdCorreoGmail { get; set; }
        public int GmailCorreoId { get; set; }
        public int IdGmailFolder { get; set; }
        public string NombreGmailFolder { get; set; }
        public bool EsLeido { get; set; }
        public string Remitente { get; set; }
        public string Destinatarios { get; set; }
        public string NombreCentroCosto { get; set; }
        public string Asunto { get; set; }
        public bool CumpleCriterioCrearOportunidad { get; set; }
        public bool AplicaCrearOportunidad { get; set; }
        public bool SeCreoOportunidad { get; set; }
        public bool? EsDesuscritoCorrectamente { get; set; }
        public bool? EsMarcadoDesuscrito { get; set; }
        public bool? EsDescartado { get; set; }
        public DateTime Fecha { get; set; }
        public string CuerpoHtml { get; set; }
        public int? TotalCorreos { get; set; }
    }

}
