using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPuestoTrabajoRemuneracionDetalle
    {
        public int Id { get; set; }
        public int IdPuestoTrabajoRemuneracion { get; set; }
        public int IdRemuneracionTipo { get; set; }
        public int IdRemuneracionTipoCobro { get; set; }
        public int IdRemuneracionFormaCobro { get; set; }
        public int IdRemuneracionPeriodoCobro { get; set; }
        public bool EsTasa { get; set; }
        public decimal? MontoFijo { get; set; }
        public int? IdMonedaMontoFijo { get; set; }
        public decimal? PorcentajeTasa { get; set; }
        public string DescripcionEquipo { get; set; }
        public bool TieneCondicion { get; set; }
        public int? IdRemuneracionDescripcionMonetaria { get; set; }
        public decimal? RangoValorMinimo { get; set; }
        public decimal? RangoValorMaximo { get; set; }
        public int? IdMonedaRangoValor { get; set; }
        public decimal? IngresoMensual { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPuestoTrabajoRemuneracion IdPuestoTrabajoRemuneracionNavigation { get; set; }
    }
}
