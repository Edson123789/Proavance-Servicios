using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComprobantePorFurDTO
    {
        public int Id { get; set; }
        public string Comprobante { get; set; }
        public string Proveedor { get; set; }
        public int IdAsociacion { get; set; }
        public string NombreAsociacion { get; set; }
        public int IdMoneda { get; set; }
        public decimal MontoAsociado { get; set; }
        public decimal MontoAmortizar { get; set; }
    }
}
