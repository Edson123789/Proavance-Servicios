using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class ResultadoDTO
    {
        public int IdAsesor { get; set; }
        public int Resultado { get; set; }
    }
    public class ResultadoFechaCompromiso
    {
        public int NroFechaCompromiso { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal Cuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaCompromiso { get; set; }
    }
}
