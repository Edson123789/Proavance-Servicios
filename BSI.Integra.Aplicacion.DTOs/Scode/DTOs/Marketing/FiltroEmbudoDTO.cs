using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroEmbudoDTO
    {
        public List<FiltroDTO> ListadoAreaCapacitacion { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> ListadoSubAreaCapacitacion { get; set; }
        public List<FiltroDTO> ListadoTipoCategoriaOrigen { get; set; }
        public List<FiltroPGeneralDTO> ListadoProgramaGeneral { get; set; }
        public List<FiltroPespecificoPGeneralDTO> ListadoProgramaEspecifico { get; set; }
        public List<FiltroDTO> ListadoPais { get; set; }
    }
}
