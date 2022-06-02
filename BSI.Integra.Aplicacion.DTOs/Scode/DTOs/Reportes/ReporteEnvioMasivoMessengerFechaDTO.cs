using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteEnvioMasivoMessengerFechaDTO
    {
        public string Fecha { get; set; }
        public int? MensajesEnviados { get; set; }
        public int? RespondidosCliente { get; set; }
        public int RespondidosAsesor { get; set; }
        public int OportunidadCreada { get; set; }
        public int OportunidadVentas { get; set; }
        public int OportunidadCerradas { get; set; }
        public int OportunidadIS { get; set; }
        public int Desuscritos { get; set; }
    }
}
