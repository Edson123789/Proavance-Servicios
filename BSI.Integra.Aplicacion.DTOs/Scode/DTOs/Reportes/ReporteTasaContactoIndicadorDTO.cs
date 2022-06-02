using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaContactoIndicadorDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public List<ReporteTasaContactoIndicadoresDTO> Lista { get; set; }
    }
    
}
