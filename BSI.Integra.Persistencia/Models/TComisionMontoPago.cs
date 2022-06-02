using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TComisionMontoPago
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonal { get; set; }
        public decimal MontoComision { get; set; }
        public int IdMoneda { get; set; }
        public int IdComercialTipoPersonal { get; set; }
        public int IdComisionEstadoPago { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TComercialTipoPersonal IdComercialTipoPersonalNavigation { get; set; }
        public virtual TComisionEstadoPago IdComisionEstadoPagoNavigation { get; set; }
        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; }
        public virtual TMoneda IdMonedaNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
    }
}
