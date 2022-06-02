using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EscalaProbabilidadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double ProbabilidadActual { get; set; }
        public double ProbabilidadInicial { get; set; }
        public int Orden { get; set; }

    }
}
