using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoCambioDTO
    {
        public int Id { get; set; }
        public double SolesDolares { get; set; }
        public double DolaresSoles { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdPeriodo { get; set; }
        public string NombreUsuario { get; set; }
    }
    public class TipoCambioFiltroDTO
    {
        public int IdMoneda { get; set; }
        public DateTime? Fecha { get; set; }
    }
    public class TipoCambioReporteDTO
    {
        public int Id { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public double MonedaADolar { get; set; }
        public double DolarAMoneda { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdPeriodo { get; set; }
        public string NombreUsuario { get; set; }
    }
}
