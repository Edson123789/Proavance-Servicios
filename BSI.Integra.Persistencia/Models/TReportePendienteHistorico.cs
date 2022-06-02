using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TReportePendienteHistorico
    {
        public int Id { get; set; }
        public string TipoReporte { get; set; }
        public int? IdPersonalCoordinadora { get; set; }
        public string UsuarioCoordinadora { get; set; }
        public int? IdPeriodo { get; set; }
        public int? MontoProyectadoInicial { get; set; }
        public int? MontoCambios { get; set; }
        public int? MontoProyectadoConCambios { get; set; }
        public int? MontoVencido { get; set; }
        public int? MontoActual { get; set; }
        public int? MontoPorVencer { get; set; }
        public int? MontoRealCronograma { get; set; }
        public int? MontoRealIngresoPrevio { get; set; }
        public int? MontoRealIngreso { get; set; }
        public int? MontoPendiente { get; set; }
        public decimal? PorcentajeProyectoInicial { get; set; }
        public decimal? PorcentajeVencido { get; set; }
        public int? MontoRetirados { get; set; }
        public int? MontoIngresoVentas { get; set; }
        public int? MontoProyectadoOperaciones { get; set; }
        public int? MontoRecuperadoMes { get; set; }
        public decimal? PorcentajeProyectadoActual { get; set; }
        public int Correlativo { get; set; }
        public DateTime FechaCierre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPeriodo IdPeriodoNavigation { get; set; }
        public virtual TPersonal IdPersonalCoordinadoraNavigation { get; set; }
    }
}
