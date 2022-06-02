using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteControlOperativoCombosDTO
    {
        public List<CoordinadorFiltroDTO> Coordinadores { get; set; }
        public List<AsesorNombreFiltroDTO> Asesores { get; set; }
        public List<FiltroDTO> Grupos { get; set; }
        
    }
}
