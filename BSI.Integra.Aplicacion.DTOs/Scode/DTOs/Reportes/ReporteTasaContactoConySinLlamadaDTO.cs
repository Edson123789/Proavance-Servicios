using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaContactoConySinLlamadaDTO
    {
        public int CambiosFaseConLlamada { get; set; }
        public int CambiosFaseTotal { get; set; }
        public int CambiosFaseSinLlamada { get; set; }
        public int CambiosFaseOCconLlamada { get; set; }
        public int CambiosFaseOCsinLlamada { get; set; }
    }
}
