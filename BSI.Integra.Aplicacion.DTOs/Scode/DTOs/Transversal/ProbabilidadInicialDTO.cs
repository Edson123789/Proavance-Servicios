using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
   public class ProbabilidadInicialDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal? Probabilidad { get; set; }
        public int? IdProbabilidadRegistroPw{ get; set; }
    }

}
