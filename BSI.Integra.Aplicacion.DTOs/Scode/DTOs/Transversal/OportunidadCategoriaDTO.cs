using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class OportunidadCategoriaDTO
    {
        public int IdOportunidad { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdPersonalAsignado { get; set; }
        public int IdProgramaGeneral { get; set; }
        public int IdAlumno { get; set; }
        public int PesoCategoria { get; set; }
    }
}
