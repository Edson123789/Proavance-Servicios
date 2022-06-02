using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MoraActualizadoDTO
    {
        public List<CronogramaFinalModificadoDTO> ListaCronograma { get; set; }
        public AdelantoMoraDTO Objeto { get; set; }
        public string Usuario { get; set; }
    }
}
