using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteProblemasAulaVirtualFiltroDTO
    {
        public List<int> CentroCostos { get; set; }
        public List<int> Coordinadores { get; set; }
        public List<int> MatriculaCabecera { get; set; }
        public List<int> TipoCategoriaError { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        
    }
}
