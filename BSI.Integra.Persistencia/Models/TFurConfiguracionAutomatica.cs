using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFurConfiguracionAutomatica
    {
        public int Id { get; set; }
        public int IdFurTipoSolicitud { get; set; }
        public int IdSede { get; set; }
        public int IdFurTipoPedido { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public decimal Cantidad { get; set; }
        public int IdMonedaPagoReal { get; set; }
        public int AjusteNumeroSemana { get; set; }
        public int IdHistoricoProductoProveedor { get; set; }
        public int IdFrecuencia { get; set; }
        public int IdCentroCosto { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaGeneracionFur { get; set; }
        public DateTime FechaInicioConfiguracion { get; set; }
        public DateTime FechaFinConfiguracion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdEmpresa { get; set; }
    }
}
