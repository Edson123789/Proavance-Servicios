using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: Gestion de Personas/PreguntaIntento
    /// Autor: Britsel Calluchi
    /// Fecha: 21/02/2021
    /// <summary>
    /// BO para la logica de las preguntas intentos de capacitacion
    /// </summary>
    public class PreguntaIntentoBO : BaseBO
    {
        /// Propiedades	                                Significado
        /// -----------	                                ------------
        /// NumeroMaximoIntento                         Numero maximo de intentos
        /// ActivarFeedbackMaximoIntento                Flag para verificar el feedback de maximo intento
        /// MensajeFeedback                             Cadena con el mensaje de Feedback
        /// IdMigracion                                 Id de migracion de V3 (nullable)
        
        public int? NumeroMaximoIntento { get; set; }
        public bool? ActivarFeedbackMaximoIntento { get; set; }
        public string MensajeFeedback { get; set; }
        public int? IdMigracion { get; set; }
    }
}
