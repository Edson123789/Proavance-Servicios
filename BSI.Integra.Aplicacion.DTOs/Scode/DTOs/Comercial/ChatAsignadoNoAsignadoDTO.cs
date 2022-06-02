using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ChatAsignadoNoAsignadoDTO
    {
        public string NombreArea { get ;set ;}
        public string NombreSubArea { get; set; }
        public string NombrePGeneral { get; set; }
        public string NombrePais { get; set; }
        public int? IdAsesorChat { get; set; }
        public bool? EsAsignado { get; set; }
        public string NombrePersonal { get; set; }
    }
}
