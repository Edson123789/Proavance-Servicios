using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegistroRankingProgramaDTO
    {
    }

    public class listaRegistroRankingProgramaDTO
    {
        public int Id { get; set; }
        public int Porcentaje { get; set; }
        public int TipoPrograma { get; set; }
        public int Cantidad { get; set; }
        public int CantidadInicio { get; set; }
        public int CantidadFinal { get; set; }
    }
}
