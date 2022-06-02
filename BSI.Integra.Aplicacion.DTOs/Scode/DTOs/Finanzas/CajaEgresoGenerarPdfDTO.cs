using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CajaEgresoGenerarPdfDTO
    {
        public int IdCajaEgresoAprobado { get; set; }
        public int IdCaja { get; set; }
        public string CodigoCaja { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Ruc { get; set; }
        public string Central { get; set; }
        public string CodigoEgresoCaja { get; set; }
        public string NombreProveedor { get; set; }
        public string RucProveedor { get; set; }
        public string CodigoFur { get; set; }
        public string Comprobantes { get; set; }
        public string FechaEmisionRecibo { get; set; }
        public string EntregadoA { get; set; }
        public string DniEntregadoA { get; set; }
        public string Responsable { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal MontoTotal { get; set; }
        public string Origen { get; set; }
        public string Moneda { get; set; }
        public int IdMoneda { get; set; }
        public string Detalle { get; set; }
        public string Observacion { get; set; }
        public string CodigosPorRendir { get; set; }
        public string TipoDocumentosSunat { get; set; }
        public string FechaGeneracionREC { get; set; }
    }
}
