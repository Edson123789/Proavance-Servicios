
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CronogramaModificadoDTO
    {
        public SolicitudCambioCronograma Objeto { get; set; }
        public List<ListaCambiosDTO> ListaCambiosOrden { get; set; }
        public List<CronogramaFinalModificadoDTO> ListaCronograma { get; set; }
        public string Usuario { get; set; }

    }
}
