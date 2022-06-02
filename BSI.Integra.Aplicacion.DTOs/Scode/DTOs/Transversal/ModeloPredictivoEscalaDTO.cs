using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModeloPredictivoEscalaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public decimal ProbabilidadActual { get; set; }
        public decimal ProbabilidaIInicial { get; set; }

    }
}
