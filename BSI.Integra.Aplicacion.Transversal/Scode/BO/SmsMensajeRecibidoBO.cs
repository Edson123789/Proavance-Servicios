using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/SmsMensajeRecibido
    /// Autor: Gian Miranda
    /// Fecha: 31/12/2021
    /// <summary>
    /// BO para la logica de los SMS recibidos
    /// </summary>
    public class SmsMensajeRecibidoBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// NumeroTelefono                      Numero del telefono del cual se recibe el mensaje
        /// Puerto                              Puerto por donde ingresa el mensaje
        /// NombrePuerto                        Nombre o alias del puerto donde ingresa el mensaje
        /// Mensaje                             Mensaje entrante
        /// FechaRecepcion                      Fecha de recepcion del mensaje
        /// Imsi                                Imsi del mensaje
        /// EstadoMensaje                       Estado del mensaje
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public string NumeroTelefono { get; set; }
        public string Puerto { get; set; }
        public string NombrePuerto { get; set; }
        public string Mensaje { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string Imsi { get; set; }
        public string EstadoMensaje { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public SmsMensajeRecibidoBO()
        {
        }

        public SmsMensajeRecibidoBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
