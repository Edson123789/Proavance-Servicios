using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfAgrComposicionPortafolio
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string Tipo { get; set; }
        public string CalidadDeudor { get; set; }
        public decimal? Porcentaje { get; set; }
        public int? Cantidad { get; set; }
        public string EstadoCodigo { get; set; }
        public int? EstadoCantidad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TDataCreditoBusqueda IdDataCreditoBusquedaNavigation { get; set; }
    }
}
