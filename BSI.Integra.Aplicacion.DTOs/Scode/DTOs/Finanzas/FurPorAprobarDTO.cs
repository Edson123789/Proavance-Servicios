using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FurPorAprobarDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public string RazonSocial { get; set; }
        public string Producto { get; set; }
        public int? IdMoneda_Proveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalDolares { get; set; }
        public string Observaciones { get; set; }
        public int? IdMonedaPagoReal { get; set; }
        public string MonedaPagoReal { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public DateTime FechaLimite { get; set; }
        public string FurTipoPedido { get; set; }
        public string UsuarioSolicitud { get; set; }
    }
}
