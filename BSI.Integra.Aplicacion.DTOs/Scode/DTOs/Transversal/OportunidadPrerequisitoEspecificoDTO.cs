using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadPrerequisitoEspecificoDTO
    {
        public int IdOportunidadCompetidor { get; set; }
        public int IdProgramaGeneralBeneficio { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
    }
}
