using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRankingIngreso
    {
        public int Id { get; set; }
        public int? Inscritos { get; set; }
        public int? Cerradas { get; set; }
        public decimal? Tcreal { get; set; }
        public decimal? Tcmeta { get; set; }
        public decimal TcrealByMeta { get; set; }
        public double? IngresoOc { get; set; }
        public double? IngresoIs { get; set; }
        public double? IngresoMeta { get; set; }
        public double? IngresoReal { get; set; }
        public double? IrbyIm { get; set; }
        public int? RankingGeneral { get; set; }
        public double? IngresoPromedioOc { get; set; }
        public int? IdCategoriaAsesor { get; set; }
        public int? RankingTipoAsesor { get; set; }
        public int? IdPersonal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
