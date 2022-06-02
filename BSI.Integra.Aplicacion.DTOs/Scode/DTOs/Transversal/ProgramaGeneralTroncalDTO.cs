using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaGeneralTroncalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdTroncalPatner { get; set; }
        public int Duracion { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }
}
