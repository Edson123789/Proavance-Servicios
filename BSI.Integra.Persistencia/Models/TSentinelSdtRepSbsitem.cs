using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSentinelSdtRepSbsitem
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDoc { get; set; }
        public string NroDoc { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Calificacion { get; set; }
        public decimal? MontoDeuda { get; set; }
        public int? DiasVencidos { get; set; }
        public DateTime? FechaReporte { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TSentinel IdSentinelNavigation { get; set; }
    }
}
