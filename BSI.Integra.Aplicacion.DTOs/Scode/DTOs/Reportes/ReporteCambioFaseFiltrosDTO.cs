using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCambioFaseFiltrosDTO
    {
        public List<int> Asesores { get; set; }
        public List<int> CentroCostos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Acumulado  { get; set; }
}
}
