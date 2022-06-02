using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProbabilidadModeloDataMiningDTO
    {
        public int IdOportunidad { get; set; }
        public decimal? Probabilidad { get; set; }
        public int? IdProbabilidaRegistroPW { get; set; }
        public string Nombre { get; set; }
        public int? IdCargo { get; set; }
        public int? IdareaFormacion { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCategoriaDato { get; set; }
    }
}
