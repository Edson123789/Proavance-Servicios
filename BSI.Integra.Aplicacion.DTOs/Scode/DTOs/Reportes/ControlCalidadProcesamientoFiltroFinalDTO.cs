using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ControlCalidadProcesamientoFiltroFinalDTO
    {
        public string Asesores { get; set; }
        public string Grupos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
