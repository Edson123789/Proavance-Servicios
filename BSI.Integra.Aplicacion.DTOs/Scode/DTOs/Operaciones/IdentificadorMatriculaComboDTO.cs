using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class IdentificadorMatriculaComboDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdOportunidad { get; set; }
        public string PEspecifico { get; set; }
        public string VersionPrograma { get; set; }
    }
}
