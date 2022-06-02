using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteContactabilidadPeriodoDTO
    {
        public int IdPersonal { get; set; }
        public int Dia { get; set; }
        public int IdCodigoPais { get; set; }
        public int TotalActividades { get; set; }
        public int TotalEjecutadas { get; set; }
        public int TasaConversion { get; set; }
        public string NombreAsesor { get; set; }
        
    }
}
