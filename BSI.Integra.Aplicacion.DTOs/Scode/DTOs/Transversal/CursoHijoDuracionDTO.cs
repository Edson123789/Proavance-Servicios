using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CursoHijoDuracionDTO
    {
        public string Nombre { get; set; } 
        public int Duracion { get; set; } 
    }

    public class CursoHijoIdDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Duracion { get; set; }
    }
}
