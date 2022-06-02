using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class CursoRelacionadoProgramaDTO
    {
        public int  Id { get; set; }
        public int IdRelacionado { get; set; }
        public string  Nombre { get; set; }
    }
}
