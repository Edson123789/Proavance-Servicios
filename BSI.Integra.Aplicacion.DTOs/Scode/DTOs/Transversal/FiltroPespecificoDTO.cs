using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroPespecificoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCentroCosto { get; set; }
    }

    public class FiltroPespecificoPGeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdProgramaGeneral { get; set; }
    }
}
