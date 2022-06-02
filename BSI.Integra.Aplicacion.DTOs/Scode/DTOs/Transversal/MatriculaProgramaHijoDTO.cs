using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaProgramaHijoDTO
    {
        public int IdPespecifico { get; set; }
        public int IdPespecificoHijo { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
    }
}
