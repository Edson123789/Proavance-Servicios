using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosAreasSubAreasMailchimpDTO
    {
        public List<FiltroDTO> Areas { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> SubAreas { get; set; }
        public List<PGeneralSubAreaFiltroDTO> ProgramaGeneral { get; set; } 
    }
}
