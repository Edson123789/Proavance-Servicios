using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListaFurPagoDTO
    {
        public int IdFur { get; set; }
        public string Codigo { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreProveedorComprobante { get; set; }        
        public int? IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int? IdCC { get; set; }
        public int IdPais { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NumeroCuenta { get; set; }
        public string DescripcionCuenta { get; set; }
        public decimal Cantidad { get; set; }
        public int MonedaFur { get; set; }
        public string NombreMonedaFur { get; set; }
        public decimal PrecioUnitarioSoles { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public decimal PrecioTotalSoles { get; set; }
        public decimal PrecioTotalDolares { get; set; }
        //public int? IdDocumentoPago { get; set; }
        public string NombreDocumento { get; set; }
        public string NumeroRecibo { get; set; }
        public string Descripcion { get; set; }
        public string NumeroComprobante { get; set; }
        //public string FechaEfectivo { get; set; }
        public decimal? PagoMonedaOrigen { get; set; }
        public decimal? PagoDolares { get; set; }
        public int FaseAprobacion { get; set; }
        public int? Antiguo { get; set; }
        public int? MonedaPagoRealizado  { get; set;}
        public string NombreMonedaPagoRealizado  { get; set;}
        public bool EstadoCancelado { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
