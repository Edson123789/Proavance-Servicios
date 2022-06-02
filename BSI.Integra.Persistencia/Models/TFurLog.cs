using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFurLog
    {
        public int Id { get; set; }
        public int IdFur { get; set; }
        public string Codigo { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int IdCiudad { get; set; }
        public string NumeroFur { get; set; }
        public int? NumeroSemana { get; set; }
        public string UsuarioSolicitud { get; set; }
        public string UsuarioAutoriza { get; set; }
        public string Observaciones { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal? Monto { get; set; }
        public decimal? MontoProyectado { get; set; }
        public int? IdProductoPresentacion { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdMonedaProveedor { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroRecibo { get; set; }
        public decimal? PagoMonedaOrigen { get; set; }
        public decimal? PagoDolares { get; set; }
        public DateTime? FechaCobroBanco { get; set; }
        public string ResponsableCobro { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Cuenta { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalDolares { get; set; }
        public int IdFurFaseAprobacion1 { get; set; }
        public int? AprobadoFase2 { get; set; }
        public DateTime? FechaLimite { get; set; }
        public int IdFurTipoPedido { get; set; }
        public bool Cancelado { get; set; }
        public int? Antiguo { get; set; }
        public int? IdMonedaPagoReal { get; set; }
        public bool? OcupadoSolicitud { get; set; }
        public bool? OcupadoRendicion { get; set; }
        public bool EstadoAprobadoObservado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdMonedaPagoRealizado { get; set; }
        public DateTime? FechaAprobacionProcesoCulminado { get; set; }
        public bool? EsDiferido { get; set; }
        public int? IdFurSubFaseAprobacion { get; set; }
        public bool EsPrimero { get; set; }
        public bool EsUltimo { get; set; }
        public DateTime? FechaLimiteReprogramacion { get; set; }
    }
}
