using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CorreoDTO
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public string EmailBody { get; set; }
        public DateTime Fecha { get; set; }
        public string Remitente { get; set; }
        public string Destinatarios { get; set; }
        public string From { get; set; }
        public bool Seen { get; set; }
        public int? TotalCorreos { get; set; }
        public int? IdPersonal { get; set; }
        public bool? EnvioMasivoMandrill { get; set; }
        public bool? EnvioIndividualMandrill { get; set; }
        public string ConCopia { get; set; }    
    }
}
