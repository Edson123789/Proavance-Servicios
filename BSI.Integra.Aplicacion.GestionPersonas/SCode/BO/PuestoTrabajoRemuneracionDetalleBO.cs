using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class PuestoTrabajoRemuneracionDetalleBO : BaseBO
	{
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
    }
}
