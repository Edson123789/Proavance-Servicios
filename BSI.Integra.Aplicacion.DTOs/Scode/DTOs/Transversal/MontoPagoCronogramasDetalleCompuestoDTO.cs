using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoCronogramasDetalleCompuestoDTO
    {
        public MontoPagoCronogramaCompuestoDTO cronograma { get; set; }
        public List<MontoPagoCronogramaDetalleDTO> listaDetalle { get;set;}        
    }
}
