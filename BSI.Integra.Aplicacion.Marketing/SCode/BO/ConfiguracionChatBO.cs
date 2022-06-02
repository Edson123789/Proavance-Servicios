using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: Marketing/ConfiguracionChat
    /// Autor: Wilber Choque - Joao Benavente - Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// BO para la logica de las configuraciones de los chats
    /// </summary>
    public class ConfiguracionChatBO : BaseBO
    {
        /// Propiedades	                    Significado
        /// -----------	                    ------------
        /// NombreConfiguracion             Nombre de la configuracion
        /// VisualizarTiempo                Tiempo para visualizar la notificacion del chat
        /// TextoHeader                     Texto que aparecera en el header
        /// TextoHeaderNotificacion         Texto que aparecera en header de la notificacion
        /// ColorFondoHeader                Color de fondo del header
        /// ColorTextoHeader                Color del texto del header
        /// TextoHeaderFuente               Fuente del header
        /// IconoAsesor                     Nombre de la imagen que mostrara al asesor
        /// ColorFondoAsesor                Color de fondo de los mensajes del asesor
        /// ColorTextoAsesor                Color del texto de los mensajes del asesor
        /// IconoInteresado                 Nombre de la imagen que mostrara al visitante
        /// ColorFondoInteresado            Color de fondo de los mensajes del interesado
        /// ColorTextoInteresado            Color del texto de los mensajes del interesado
        /// TextoChatFuente                 Fuente del texto en el chat
        /// TextoOffline                    Mensaje que se mostrara en caso no haya asesor conectado
        /// TextoSatisfaccionOffline        Texto que se mostrara cuando envie un mensaje en modo offline el interesado
        /// TextoInicial                    Texto por defecto que aparecera en el chat
        /// ColorTextoEmpezarChat           Color del fondo en el boton Empezar a Chatear
        /// ColorFondoEmpezarChat           Color del fondo en el boton Empezar a Chatear
        /// TextoFormularioFuente           Fuente del texto en el formulario
        /// IconoChat                       Nombre de la imagen que se muestra en el chat
        /// MuestraTextoInicial             Flag si muestra o no el mensaje por defecto
        /// Estado                          Estado del registro
        /// UsuarioCreacion                 Usuario de creacion
        /// UsuarioModificacion             Usuario de modificacion
        /// FechaCreacion                   Fecha de creacion
        /// FechaModificacion               Fecha de modificacion
        /// RowVersion                      Timestamp para determinar la version
        /// IdMigracion                     Id de migracion de V3, campo nullable

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
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
