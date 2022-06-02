using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CuentasPorPagarDTO
    {
        public string Id { get; set; }
        public string Empresa{ get; set; }
        public string Sede{ get; set; }
        public string Area{ get; set; }
        public string TipoPedido{ get; set; }
        public string CodigoFur{ get; set; }
        public string ProductoServicio{ get; set; }
        public decimal Cantidad{ get; set; }
        public string Unidades{ get; set; }
        public decimal PrecioUnitario{ get; set; }
        public string MonedaFur{ get; set; }
        public string Descripcion{ get; set; }
        public string CentroCosto{ get; set; }
        public string Curso{ get; set; }
        public string Programa{ get; set; }
        public string Atipico{ get; set; }
        public string Rubro{ get; set; }
        public long NroCuenta{ get; set; }
        public string Cuenta{ get; set; }
        public string UsuarioCreacion{ get; set; }
        public string UsuarioModificacion{ get; set; }
        public string TipoDocumento{ get; set; }
        public string NroDoc{ get; set; }
        public string Proveedor{ get; set; }
        public string TipoComprobante{ get; set; }
        public string NumComprobante{ get; set; }
        public string MonedaFurComprobante{ get; set; }
        public decimal MontoporPagar{ get; set; }
        public decimal? TotalDolares{ get; set; }
        public DateTime? FechaEmisionComprobante{ get; set; }
        public string MesdeEmision{ get; set; }
        public DateTime? FechaVencimientoComprobante{ get; set; }
        public string MesdeVcto{ get; set; }
        public string Estado{ get; set; }
        public bool? Diferido{ get; set; }
        public string Anterior{ get; set; }
        public DateTime FechaProgramacion { get; set; }
        public string MesProgramacion { get; set; }
        public int? IdComprobante { get; set; }
    }
}