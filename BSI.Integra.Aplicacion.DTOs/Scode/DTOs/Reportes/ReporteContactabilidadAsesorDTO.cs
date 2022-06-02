using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadAsesorDTO
    {
        public int Hora { get; set; }
        public int IdAsesor { get; set; }
        public string NombreAsesor { get; set; }
        public double TC { get; set; }
        public string Clave { get; set; }
        public int AT { get; set; }
        public int TE { get; set; }
    }
}
