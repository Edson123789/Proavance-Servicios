using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MessengerAsesorDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public string Usuario { get; set; }
        public int[] IdAreaCapacitacionFacebook { get; set; }
    }
}
