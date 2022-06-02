using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosAmbienteDTO
    {
        public List<LocacionComboFiltroDTO> LocacionComboFiltro { get; set; }
        public List<TipoAmbienteFiltroDTO> TipoAmbienteFiltro { get; set; }
    }
}
