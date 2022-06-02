using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsesorChatDetalleDetalleDTO
    {
        public List<IdDTO> listadoIdsPais { get; set; }
        public List<IdDTO> listadoIdsProgramaGeneral { get; set; }
        public List<IdDTO> listadoIdsAreaCapacitacion { get; set; }
        public List<IdDTO> listadoIdsSubAreaCapacitacion { get; set; }
    }
}
