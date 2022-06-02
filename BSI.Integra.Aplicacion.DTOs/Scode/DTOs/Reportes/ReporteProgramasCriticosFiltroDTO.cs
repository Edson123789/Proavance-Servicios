using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteProgramasCriticosFiltroDTO
    {
        public List<int> Grupos { get; set; }
        public List<int> Areas { get; set; }
        public List<int> Subareas { get; set; }
        public List<int> Pais { get; set; }
        public List<string> EstadoPrograma { get; set; }
        public List<int> Periodo { get; set; }

    }
}
