using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoDTO
    {
        public int IdPGeneral { get; set; }
        public int Paquete { get; set; }
        public string NombrePaquete { get; set; }
        public string InversionContado { get; set; }
        public string InversionCredito { get; set; }
        public double ContadoByDolares { get; set; }
        public int Pais { get; set; }
        public string Beneficios { get; set; }
    }
}
