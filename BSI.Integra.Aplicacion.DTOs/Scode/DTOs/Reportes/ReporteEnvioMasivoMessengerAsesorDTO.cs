using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteEnvioMasivoMessengerAsesorDTO
    {
        public string Asesor { get; set; }
        public string IdPersonal { get; set; }
        public List<ReporteEnvioMasivoMessengerFechaDTO> Detalle { get; set; }
    }
}
