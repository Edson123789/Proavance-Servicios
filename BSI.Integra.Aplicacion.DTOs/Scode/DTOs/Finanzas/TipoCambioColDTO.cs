using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoCambioColDTO
    {
        public int Id { get; set; }
        public double PesosDolares { get; set; }
        public double DolaresPesos { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMoneda { get; set; }
        public string NombreUsuario { get; set; }
        
    }
}
