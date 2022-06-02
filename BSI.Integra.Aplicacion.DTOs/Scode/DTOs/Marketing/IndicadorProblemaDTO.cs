using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class IndicadorProblemaDTO
    {
        public int Id { get; set; }
        public int IdProblema { get; set; }
        public int IdIndicador { get; set; }
        public int IdOperadorComparacion { get; set; }
        public decimal Valor { get; set; }
        public int MuestraMinima { get; set; }
        public string Usuario { get; set; }
    }
}
