using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ExpositorPorAreaListadoDTO
    {
        public int Id { get; set; }
        public int IdExpositor { get; set; }
        public string NombreExpositor { get; set; }
        public int IdArea { get; set; }
        public string NombreArea { get; set; }
        public string Usuario { get; set; }
    }
}
