using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaGeneralBeneficioOportunidadDTO
    {
        public int IdBeneficio { get; set; }
        public string NombrePrerequisito { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public List<ProgramaGeneralBeneficioArgumentoDTO> Argumentos { get; set; }
    }
}
