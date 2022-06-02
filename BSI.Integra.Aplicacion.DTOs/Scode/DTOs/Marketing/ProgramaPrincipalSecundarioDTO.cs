using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaPrincipalSecundarioDTO
    {
        public List<FiltroPGeneralDTO> Principal { get; set; }
        public List<FiltroPGeneralDTO> Secundario { get; set; }
    }
}
