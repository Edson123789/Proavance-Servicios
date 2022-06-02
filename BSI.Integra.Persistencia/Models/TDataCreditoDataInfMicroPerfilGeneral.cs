using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfMicroPerfilGeneral
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string Tipo { get; set; }
        public string SectorFinanciero { get; set; }
        public string SectorCooperativo { get; set; }
        public string SectorReal { get; set; }
        public string SectorTelcos { get; set; }
        public string TotalComoPrincipal { get; set; }
        public string TotalComoCodeudorYotros { get; set; }
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
