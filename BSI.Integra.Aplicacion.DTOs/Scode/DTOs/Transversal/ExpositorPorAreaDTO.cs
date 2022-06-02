using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ExpositorPorAreaDTO
    {
        public int Id { get; set; }
        public int IdExpositor { get; set; }
        public List<int> IdArea { get; set; }
        public string Usuario { get; set; }
    }
}
