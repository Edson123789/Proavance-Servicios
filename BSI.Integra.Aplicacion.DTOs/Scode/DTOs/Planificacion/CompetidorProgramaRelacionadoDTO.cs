using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompetidorProgramaRelacionadoDTO
    {
        public int Id { get; set; }
        public int? IdCompetidor { get; set; }
        public int IdPrograma { get; set; }
    }
}
