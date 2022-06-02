using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCambiosDeFaseOportunidadDTO
    {
        public int? Numero { get; set; }
        public int NumeroRegistros { get; set; }
        public string FaseOrigen { get; set; }
        public string FaseDestino { get; set; }
        public string TipoDato { get; set; }
        public decimal MetaLanzamiento { get; set; }
        public int IndicadorLanzamiento { get; set; }
    }
}
