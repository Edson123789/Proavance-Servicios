using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GenericoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
    }


    public class GenericoUrlDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Usuario { get; set; }
    }
}
