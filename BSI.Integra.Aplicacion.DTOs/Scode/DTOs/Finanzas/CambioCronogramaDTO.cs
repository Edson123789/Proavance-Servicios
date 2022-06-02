using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CambioCronogramaDTO
    {
        public string TipoModificacion { get; set; }
        public string SubTipo { get; set; }
        public string EmailAprueba { get; set; }
        public string EmailSolicita { get; set; }
    }
}
