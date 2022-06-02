using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComisionMontoPagoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonal { get; set; }
        public decimal MontoComision { get; set; }
        public int IdMoneda { get; set; }
        public int IdComercialTipoPersonal { get; set; }
        public int IdComisionEstadoPago { get; set; }
        public string Observacion { get; set; }
        
    }
}
