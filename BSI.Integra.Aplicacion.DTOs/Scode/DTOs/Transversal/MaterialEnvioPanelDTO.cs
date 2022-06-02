using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialEnvioPanelDTO
    {
        public int Id { get; set; }
        public int IdSedeTrabajo { get; set; }
        public string NombreSedeTrabajo { get; set; }
        public int IdPersonalRemitente { get; set; }
        public string NombrePersonalRemitente { get; set; }
        public int IdProveedorEnvio { get; set; }
        public string NombreProveedorEnvio { get; set; }
        public DateTime FechaEnvio { get; set; } 
    }
}
