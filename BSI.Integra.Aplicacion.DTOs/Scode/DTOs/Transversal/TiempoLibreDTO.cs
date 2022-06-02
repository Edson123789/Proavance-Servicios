using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TiempoLibreDTO
    {
        public int Id { get; set;}
        public int TiempoMin { get; set; }
        public int Tipo { get; set; }
        public string Usuario { get; set; }
    }
}
