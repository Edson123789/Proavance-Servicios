using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCompromisoPagoDTO
    {
        public List<int> ListaCoordinador { get; set; }
        public List<int> ListaAlumno { get; set; }
        public List<int> ListaCentroCosto { get; set; }
        public int? EstadoCuotas { get; set; }
        public DateTime? FechaGeneradoInicio { get; set; }
        public DateTime? FechaGeneradoFin { get; set; }
        public DateTime? FechaCompromisoInicio { get; set; }
        public DateTime? FechaCompromisoFin { get; set; }
        public string CodigoMatricula { get; set; }
        public int Personal { get; set; }
    }

    public class ReporteCompromisoPagoDetalleDTO
    {
        public int Id { get; set; }
        public string Coordinadora { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string CentroCosto { get; set; }
        public string TipoCuota { get; set; }
        public int? NroCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int? FechaVencimientoAnio { get; set; }
        public int? FechaVencimientoMes { get; set; }
        public int? FechaVencimientoDia { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public string HoraCompromiso { get; set; }
        public decimal? CuotaVencidaMesActual { get; set; }
        public decimal? CuotaVencidaMesAnterior { get; set; }
        public decimal? CuotaVencidaHasta12 { get; set; }
        public decimal? CuotaVencidaMas12 { get; set; }
        public decimal? CuotaVencidaMesAdelantado { get; set; }
        public DateTime? FechaPago { get; set; }
        public string MonedaCuota { get; set; }
        public decimal? MontoCuota { get; set; }
        public decimal? MontoCuotaDolares { get; set; }
        public DateTime? FechaGeneracionCompromiso { get; set; }
        public string AgendaTab { get; set; }
        public string NombrePais { get; set; }
        public int? CantidadCompromisos { get; set; }
        public decimal? MontoPagadoDolares { get; set; }
        public string EstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public decimal? MontoCompromisoDolares { get; set; }
    }

    public class GenerarReporteCompromisoPagoFiltroGrillaDTO
    {
        public Paginador Paginador { get; set; }
        public GridFilters Filter { get; set; }
        public ReporteCompromisoPagoDTO Filtro { get; set; }
    }

    public class ResultadoFiltroReporteCompromisoDTO
    {
        public List<ReporteCompromisoPagoDetalleDTO> Lista { get; set; }
        public int Total { get; set; }
    }

    public class TotalReporteCompromisoPagoDTO
    {
        public int Cantidad { get; set; }
    }
}
