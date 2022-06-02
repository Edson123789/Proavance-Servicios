using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoCronogramaGrupalGrupoDTO
    {
        public int Grupo { get; set; }
        public List<PEspecificoCronogramaGrupalDTO> lista { get; set; }
    }
}
