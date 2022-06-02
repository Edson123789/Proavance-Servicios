using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfiguracionChatDTO
    {
        public int Id { get; set; }
        public string NombreConfiguracion { get; set; }
        public int? VisualizarTiempo { get; set; }
        public string TextoHeader { get; set; }
        public string TextoHeaderNotificacion { get; set; }
        public string ColorFondoHeader { get; set; }
        public string ColorTextoHeader { get; set; }
        public string TextoHeaderFuente { get; set; }
        public string IconoAsesor { get; set; }
        public string ColorFondoAsesor { get; set; }
        public string ColorTextoAsesor { get; set; }
        public string IconoInteresado { get; set; }
        public string ColorFondoInteresado { get; set; }
        public string ColorTextoInteresado { get; set; }
        public string TextoChatFuente { get; set; }
        public string TextoOffline { get; set; }
        public string TextoSatisfaccionOffline { get; set; }
        public string TextoInicial { get; set; }
        public string ColorTextoEmpezarChat { get; set; }
        public string ColorFondoEmpezarChat { get; set; }
        public string TextoFormularioFuente { get; set; }
        public string IconoChat { get; set; }
        public int MuestraTextoInicial { get; set; }
        public string NombreUsuario { get; set; }
    }
}
