using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteWhatsAppMasivoFiltrosDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdPersonal { get; set; }
        public int IdPais  { get; set; }
    }
}
