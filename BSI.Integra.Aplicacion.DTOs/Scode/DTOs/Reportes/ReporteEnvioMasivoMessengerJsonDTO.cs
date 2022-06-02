using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteEnvioMasivoMessengerJsonDTO
    {
        public string Fecha { get; set; }
        public string    IdPersonal { get; set; }
        public string IdFacebookPagina { get; set; }
        public string Pais { get; set; }
        public string Asesor { get; set; }
        public int RespondidosAsesor { get; set; }
        public int OportunidadCreada { get; set; }
        public int OportunidadVentas { get; set; }
        public int OportunidadCerradas { get; set; }
        public int OportunidadIS { get; set; }
        public int Desuscritos { get; set; }
    }
}
