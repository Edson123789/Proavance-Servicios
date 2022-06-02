using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCambioFaseFiltroProcedimientoDTO
    {
        public string IdPersonal { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdCategoriaOrigen { get; set; }
        public string IdTipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        
    }
}
