using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadLogDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPersonalAsignado { get; set; }
        public int IdTipoDato { get; set; }
        public int IdOrigen { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdFaseOportunidadAnt { get; set; }
    }
}
