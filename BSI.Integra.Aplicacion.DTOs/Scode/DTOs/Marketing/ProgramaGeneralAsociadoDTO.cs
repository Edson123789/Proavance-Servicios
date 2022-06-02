using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaGeneralAsociadoDTO
    {
        public int IdAsesorCentroCosto { get; set; }
        public int Prioridad { get; set; }
        public int IdPGeneral { get; set; }
        public string NombrePGeneral { get; set; }
        public int CantidadIdPGeneral { get; set; }
    }
}
