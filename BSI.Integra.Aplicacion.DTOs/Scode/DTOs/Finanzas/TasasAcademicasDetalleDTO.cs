using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TasasAcademicasDetalleDTO
    {
        public string CodigoMatricula { get; set; }
        public int IdConcepto { get; set; }
        public float Monto { get; set; }
        public string Moneda { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
