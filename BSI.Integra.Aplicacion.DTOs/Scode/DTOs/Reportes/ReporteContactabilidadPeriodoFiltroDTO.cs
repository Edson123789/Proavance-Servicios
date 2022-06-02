using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadPeriodoFiltroDTO
    {
        public int Periodo { get; set; }
        public List<int> Asesores { get; set; }
        public int Tipo { get; set; }
    }
}
