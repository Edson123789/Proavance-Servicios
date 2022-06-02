using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FeriadoComboDTO
    {
        public List<CiudadTroncalPaisFiltroDTO> Ciudades { get; set; }
        public List<TipoFeriadoFiltroDTO> Tipos { get; set; }
        public List<FrecuenciaFeriadoFiltroDTO> Frecuencia { get; set; }

    }
}
