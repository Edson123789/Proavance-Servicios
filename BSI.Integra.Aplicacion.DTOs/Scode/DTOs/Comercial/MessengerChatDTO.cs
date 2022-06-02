using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MessengerChatDTO
    {
        public int Id { get; set; }
        public string PSID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public int? IdAlumno { get; set; }
        public string UrlFoto { get; set; }
        public int IdPersonal { get; set; }
        public bool SeRespondio { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string FacebookPaginaId { get; set; }
        public string Pagina { get; set; }
        public bool Desuscrito { get; set; }
        public bool TieneMasivo { get; set; }
        public int? IdMessengerChatMasivo { get; set; }

    }
}
