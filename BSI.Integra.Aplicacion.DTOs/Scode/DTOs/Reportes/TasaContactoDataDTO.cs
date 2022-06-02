using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class TasaContactoDataDTO
    {
        public string Indicador { get; set; }
        public int Orden { get; set; }
        public int? Hoy { get; set; }
        public int? Hace_5dias { get; set; }
        public int? Hace_4dias { get; set; }
        public int? Hace_3dias { get; set; }
        public int? Hace_2dias { get; set; }
        public int? Hace_1dias { get; set; }
        public DateTime? Hoy_date { get; set; }
        public DateTime? Hace_5_date { get; set; }
        public DateTime? Hace_4_date { get; set; }
        public DateTime? Hace_3_date { get; set; }
        public DateTime? Hace_2_date { get; set; }
        public DateTime? Hace_1_date { get; set; }

        public List<ReporteTasaContactoIndicadoresDTO> Lista { get; set; }
    }
}
