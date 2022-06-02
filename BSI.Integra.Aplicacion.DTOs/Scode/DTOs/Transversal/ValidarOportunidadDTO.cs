using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ValidarOportunidadDTO
    {
        public int IdAsesor { get; set; }
        public int IdCategoria { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdTipoDato { get; set; }
        public int IdOportunidad { get; set; }
        public int EstadoIsM { get; set; }
    }
}
