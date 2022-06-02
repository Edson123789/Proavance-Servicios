using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoModalidadDTO
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
    public class MontoPagadoDTO
    {
        public int Id { get; set; }
        public int IdMontoPago { get; set; }
        public string Moneda { get; set; }
        public decimal CostoOriginal { get; set; }
        public decimal Descuento { get; set; }
        public string PorcentajeDescuento { get; set; }
        public decimal CostoFinal { get; set; }
       
    }
}
