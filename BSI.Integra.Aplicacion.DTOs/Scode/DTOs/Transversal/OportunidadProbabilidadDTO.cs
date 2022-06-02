using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class OportunidadProbabilidadDTO
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneral { get; set; }
        public int IdAlumno { get; set; }
        public int PesoProbabilidad { get; set; }
    }
}
