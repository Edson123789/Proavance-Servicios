using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReportePagosDiaPeriodoDTO
    {
        public DateTime? FechaPagoDia { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public string PeriodoFechaPagoDia { get; set; }
        public string PeriodoFechaVencimiento { get; set; }

        public string Actual { get; set; }
        public string MontoPagado { get; set; }
        public string MontoPendiente { get; set; }
        public string ActualConAtrasos { get; set; }
        public string ActualSinAtrasos { get; set; }
        public string TotalPagadoDentroDelMes { get; set; }

    }
    public class ReportePagosDiaPeriodoAgrupadoDTO
    {
        public string Periodo { get; set; }
        public List<ReportePagosDiaPeriodoAgrupadoDetalleFechaDTO> DetalleFecha { get; set; }
    }
    public class ReporteEstadoAlumnosAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteAvanceAcademicoPresencialOnlineDTO> Detalle { get; set; }
    }
    public class ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> Detalle { get; set; }
    }
    public class ReporteIndicadoresOperativosAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteIndicadoresOperativosDTO> Detalle { get; set; }
    }
    public class ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO
    {
        public string Dia { get; set; }
        public List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> Detalle { get; set; }
    }
    public class ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO
    {
        public string Coordinadora { get; set; }
        public string Dia { get; set; }
        public List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> Detalle { get; set; }
    }
    public class ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> Detalle { get; set; }
    }
    public class ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteAvanceAcademicoAlumnosPagosAtrasados> Detalle { get; set; }
    }

    public class ReportePagosDiaPeriodoAgrupadoDetalleFechaDTO
    {
        public string FechaVencimiento { get; set; }

        public string PeriodoFechaVencimiento { get; set; }

        public string Actual { get; set; }
        public string MontoPagado { get; set; }
        public string MontoPendiente { get; set; }
        public string ActualConAtrasos { get; set; }
        public string ActualSinAtrasos { get; set; }
        public string TotalPagadoDentroDelMes { get; set; }

    }

    public class PagosDiaPeriodoGeneralDTO
    {
        public List<ReportePagosDiaPeriodoDTO> Periodo { get; set; }
        public List<ReportePagosDiaPeriodoDTO> PeriodoMeses { get; set; }
    }

    public class ReportePagosDiaPeriodoCompuestoDTO
    {
        public IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> ReportePagosPorDia { get; set; }
        public IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> ReportePagosPorPeriodo { get; set; }
    }
    public class ReporteEstadosAlumnosDTO
    {
        public IEnumerable<ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO> ReporteAvanceAcademicoPagos { get; set; }
        public IEnumerable<ReporteEstadoAlumnosAgrupadoDTO> ReporteAvanceAcademicoAonline { get; set; }
        public IEnumerable<ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO> ReporteAvanceAcademicoVSPagosAonline { get; set; }
        public IEnumerable<ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO> ReporteAvanceAcademicoAlumnosPagoAtrasado { get; set; }
    }
    public class ReporteIndicadoresOperativosFinalDTO
    {
        public IEnumerable<ReporteIndicadoresOperativosAgrupadoDTO> ReporteIndicadoresOperativos { get; set; }
        //public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosGeneral{ get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosACollao { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosAdelaCruz { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosBMamani { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosKAmanca { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosESanchez { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosESoto { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosGAguirre { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosKDelgado { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosPTeran { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosPRojas { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosRYMejia { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosSAlvarez { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosABerlanga { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosYRivera { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosMAmoros { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosIMurillo { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosVSanchezr { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosMCordovab { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosJRoa { get; set; }

        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosALazod { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosVCalcina { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosJLeal { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosABarrerav { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosMArrospide { get; set; }

        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosASesor03 { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosASesor04 { get; set; }

        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosAUtoperaciones { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosJChavez { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosVCaceres { get; set; }

        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosJSilgado { get; set; }

        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosRCcana { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosEFarfanp { get; set; }

        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosMParra { get; set; }

        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosDFarias { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosJCastilloc { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosYUrena { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosMCadena { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosSArpasi { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosALazaro { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosGArmas { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosYGonzalez { get; set; }
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> ReporteIndicadoresPorDiaCoordinadorOperativosRCalderons { get; set; }
        public List<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO> ReporteIndicadoresOperativosAgrupadoPorDia { get; set; }
        public List<String> Coordinadoras { get; set; }
    }
}
