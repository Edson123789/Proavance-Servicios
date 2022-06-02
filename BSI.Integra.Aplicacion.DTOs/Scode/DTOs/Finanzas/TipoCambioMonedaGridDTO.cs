using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoCambioMonedaGridDTO
    {
        public int  Id { get; set; }       
        public string NombreMoneda { get; set; }
        public int IdMoneda { get; set; }
        public double DolarAMoneda { get; set; }
        public double MonedaADolar { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
