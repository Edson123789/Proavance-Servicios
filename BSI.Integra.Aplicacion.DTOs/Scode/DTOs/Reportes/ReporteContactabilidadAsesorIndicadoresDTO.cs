using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadAsesorIndicadoresDTO
    {
        public int IdAsesor { get; set; }
        public int Hora { get; set; }
        public string Clave { get; set; }
        public int Valor { get; set; }
        public int Tipo { get; set; }
        public int? TotalLlamadas { get; set; }
    }
}
