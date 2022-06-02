using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteControlCalidadProcesamientoFiltrosDTO
    {
        public List<int> Coordinadores { get; set; }
        public List<int> Asesores { get; set; }
        public List<int> Grupos { get; set; }

    }
}
