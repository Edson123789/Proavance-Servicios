using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametroFurPorAprobarDTO
    {
        public int IdArea { get; set; }
        public string Codigo { get; set; }
        public int IdRol { get; set; }
        public int Respuesta { get; set; }
        public string Usuario { get; set; }
    }
}
