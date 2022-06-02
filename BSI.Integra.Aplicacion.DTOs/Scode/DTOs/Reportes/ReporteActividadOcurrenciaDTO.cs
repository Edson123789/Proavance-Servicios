using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteActividadOcurrenciaDTO
    {
        public int IdOportunidad { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public int? IdFaseOportunidadAnterior { get; set; }
        public int IdFaseActual { get; set; }
        public DateTime FechaReal { get; set; }

    }
}
