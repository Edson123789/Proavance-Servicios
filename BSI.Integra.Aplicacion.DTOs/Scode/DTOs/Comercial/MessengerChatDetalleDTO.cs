using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MessengerChatDetalleDTO
    {
        public int Id { get; set; }
        public int IdMeesengerUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public string Mensaje { get; set; }
        public bool Tipo { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string NombreUsuarioFB { get; set; }
        public string EmailAsesor { get; set; }
    }
}
