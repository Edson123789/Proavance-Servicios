using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoCambioMonedaDTO
    {
        public int Id { get; set; }
        public double MonedaAdolar { get; set; }
        public double DolarAmoneda { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdMoneda { get; set; }
        public int IdPeriodo { get; set; }
        //public int? IdTipoCambioCol { get; set; }
        public string NombreUsuario { get; set; }
    }
}
