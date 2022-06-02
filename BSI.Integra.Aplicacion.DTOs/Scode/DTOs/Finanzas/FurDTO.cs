using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FurDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }        
        public int? IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public int IdCiudad { get; set; }
        public int IdFurTipoPedido { get; set; }
        public int? NumeroSemana { get; set; }
        public int? IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public int? IdProducto { get; set; }
        public string Producto { get; set; }
        public int? IdProductoPresentacion { get; set; }
        public string ProductoPresentacion { get; set; }
        public int ? IdMoneda_Proveedor { get; set; }
        public string FechaLimite { get; set; }
        public string Descripcion { get; set; }
        public string NumeroCuenta { get; set; }
        public string Cuenta { get; set; }
        public decimal Cantidad { get; set; }
        public int IdFaseAprobacion1 { get; set; }
        public string FaseAprobacion1 { get; set; }
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalDolares { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Observaciones { get; set; }
        public int? IdMonedaPagoReal { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdCondicionTipoPago { get; set; }
        public string MonedaPagoReal { get; set; }
        public int? IdEmpresa { get; set; }
    }
}
