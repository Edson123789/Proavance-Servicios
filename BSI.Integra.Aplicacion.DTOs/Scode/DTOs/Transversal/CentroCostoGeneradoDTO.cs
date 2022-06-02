using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentroCostoGeneradoDTO
    {
        public CentroCosto2DTO CentroCosto { get; set; }
        public string Codigo { get; set; }
        public string NombreProgramaEspecifico { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public string CodigoBanco { get; set; }
    }
}
