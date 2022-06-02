using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CursosRelacionadosDTO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public string Modalidad { get; set; }
        public string Url_Video { get; set; }
        public string Inversion { get; set; }
    }
}
