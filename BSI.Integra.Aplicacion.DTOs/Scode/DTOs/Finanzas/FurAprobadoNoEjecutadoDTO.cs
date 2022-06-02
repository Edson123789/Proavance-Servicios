using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FurAprobadoNoEjecutadoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public string Ciudad { get; set; }
        public string TipoPedido { get; set; }
        public string RazonSocial { get; set; }
        public string Producto { get; set; }
        public string ProductoPresentacion { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Descripcion { get; set; }
        public string NumeroCuenta { get; set; }
        public string Cuenta { get; set; }
        public decimal Cantidad { get; set; }
        public string FaseAprobacion1 { get; set; }
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public string UsuarioSolicitud{ get; set; }
        public string MonedaPagoReal { get; set; }
        public DateTime FechaAprobacionJefeFinanzas { get; set; }
    }
}
