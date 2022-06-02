using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CajaPorRendirGenerarPdfDTO
    {
        public int IdPorRendirCabecera { get; set; }
        public int IdCaja { get; set; }
        public string CodigoCaja { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Ruc { get; set; }
        public string Central { get; set; }
        public string PersonalResponsable { get; set; }
        public string CuentaCaja { get; set; }
        public string Moneda { get; set; }
        public int IdMoneda { get; set; }
        public string CodigoPorRendir { get; set; }
        public string CodigoFur { get; set; }
        public string FechaAprobacion { get; set; }
        public string EntregadoA { get; set; }
        public decimal MontoTotal { get; set; }
        public string Detalle { get; set; }
        public string Observacion { get; set; }
        public int TotalReciboEgreso { get; set; }
        public decimal MontoDevolucion { get; set; }
        public decimal MontoPendienteRendicion { get; set; }
        public string FechasRendicion { get; set; }
        public string DniSolicitante { get; set; }
        public string CodigoCajaEgreso { get; set; }


    }
}
