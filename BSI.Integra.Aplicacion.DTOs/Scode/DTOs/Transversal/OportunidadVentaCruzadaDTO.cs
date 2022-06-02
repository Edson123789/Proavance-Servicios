using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadVentaCruzadaDTO
    {
        public int IdOportunidad { get; set; }
        public string Programa { get; set; }
        public string Probabilidad { get; set; }
        public string Precio { get; set; }
        public string Matricula { get; set; }
        public string Comision { get; set; }
        public string Contado { get; set; }
        public int Orden { get; set; }
        public decimal Costo { get; set; }
    }

    public class OportunidadAsignacionAutomaticaDTO
    {
        public int IdOportunidad { get; set; }
        public int? IdAsignacionAutomatica { get; set; }
    }

    public class FechaARevisarDTO
    {
        public DateTime FechaARevisar { get; set; }
    }
}
