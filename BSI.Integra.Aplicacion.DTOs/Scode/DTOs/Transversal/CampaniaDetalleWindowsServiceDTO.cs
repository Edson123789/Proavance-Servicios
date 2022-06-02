using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampaniaDetalleWindowsServiceDTO
    {
        public int IdCampaniaMailingDetalle { get; set; }
        public int IdCampaniaMailing { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int EstadoEnvio { get; set; }
        public string FechaCompleta { get; set; }
    }
}
