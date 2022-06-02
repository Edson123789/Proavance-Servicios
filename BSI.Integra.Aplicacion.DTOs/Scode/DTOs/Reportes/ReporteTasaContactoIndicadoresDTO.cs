using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaContactoIndicadoresDTO
    {
        public int Indicador { get; set; }
        public int Asesor { get; set; }
        public int Valor { get; set; }
        public int Dia { get; set; }
    }
}
