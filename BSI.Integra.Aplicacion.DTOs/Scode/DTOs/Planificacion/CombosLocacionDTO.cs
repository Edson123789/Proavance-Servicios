using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosLocacionDTO
    {
        public List<PaisFiltroParaComboDTO> PaisFiltroParaCombo { get; set; }
        public List<CiudadFiltroComboDTO> CiudadFiltroCombo { get; set; }
        public List<RegionCiudadFiltroComboDTO> RegionCiudadFiltroCombo { get; set; }
    }
}
