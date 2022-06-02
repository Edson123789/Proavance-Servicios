using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class CorreoInteraccionesAlumnoDTO
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Categoria { get; set; }
        public string Asunto { get; set; }
        public string Estado { get; set; }
        public string CorreoReceptor { get; set; }
        public string CorreoRemitente { get; set; }
        public string Remitente { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public string MessageId { get; set; }
        public int Orden { get; set; }

    }

    public class CorreoAlumnoSpeechDTO
    {
        public string Remitente { get; set; }
        public string Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string EmailBody { get; set; }
    }
}
