using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AdelantoMoraDTO
    {
        public string CodigoMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public int version { get; set; }
        public int Id { get; set; }
        public decimal MontoAdelanto { get; set; }
    }
}
