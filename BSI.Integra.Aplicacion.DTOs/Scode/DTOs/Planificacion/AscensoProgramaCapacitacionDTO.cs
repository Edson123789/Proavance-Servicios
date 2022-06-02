using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AscensoProgramaCapacitacionDTO
    {
        public int Id { get; set; }
        public int IdAscenso { get; set; }
        public int? IdProgramaCapacitacion { get; set; }
        public string Contenido { get; set; }
    }
}
