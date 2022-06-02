using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ReporteResumenMontosDTO
	{
        public string PeriodoPorFechaVencimiento { get; set; }
        public int IdTroncalPais { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }

        public decimal Proyectado { get; set; }
        public decimal Retiro_CD { get; set; }
        public decimal Retiro_SD { get; set; }

        public decimal DiferenciaCambioFecha { get; set; }
        public decimal DiferenciaCambioMonto { get; set; }
        public decimal DiferenciaConsiderarMoraAdelantoSgteCuota { get; set; }
        public decimal DiferenciaModificacionNroCuotas { get; set; }
    }

    public class ReporteResumenMontosModalidadesDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public int IdTroncalPais { get; set; }
        public int IdTipoModalidad { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }

        public decimal Proyectado { get; set; }
        public decimal Retiro_CD { get; set; }
        public decimal Retiro_SD { get; set; }

        public decimal DiferenciaCambioFecha { get; set; }
        public decimal DiferenciaCambioMonto { get; set; }
        public decimal DiferenciaConsiderarMoraAdelantoSgteCuota { get; set; }
        public decimal DiferenciaModificacionNroCuotas { get; set; }
    }

    public class ReporteResumenMontosGeneralDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public decimal ActualTotal { get; set; }
        public decimal ProyAlumnos { get; set; }
        public decimal? ProyInhouse { get; set; }

        public decimal PendienteTotal { get; set; }
        public decimal? PendienteInhouse { get; set; }
        public decimal PendienteAlumno { get; set; }

        public decimal MontoPagadoTotal { get; set; }
        public decimal MontoPagadoAlumnos { get; set; }
        public decimal? MontoPagadoInhouse { get; set; }

    }


    public class ReporteResumenMontosPagosDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public int IdTroncalPais { get; set; }
        public int IdTipoModalidad { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }

        public decimal Proyectado { get; set; }
        public decimal Retiro_CD { get; set; }
        public decimal Retiro_SD { get; set; }
                
        public decimal DiferenciaCambioFecha { get; set; }
        public decimal DiferenciaCambioMonto { get; set; }
        public decimal DiferenciaConsiderarMoraAdelantoSgteCuota { get; set; }
        public decimal DiferenciaModificacionNroCuotas { get; set; }


        public decimal DiferenciaPorModificacion { get; set; }
        public int NuevaConsultoria { get; set; }
        public decimal NuevasMatriculas { get; set; }
        public decimal IngresoRealNuevasMatriculas { get; set; }
        public decimal IngresoRealNuevasMatriculasFechaPago { get; set; }
        public decimal PendientMesOrdenServicio { get; set; }
        public decimal PendientMesSinOrdenServicio { get; set; }
        public decimal RetirosCD_Mes { get; set; }
        public decimal RetirosSD_Mes { get; set; }
        public decimal IncrementosDisminucionesCronograma { get; set; }
        public decimal ModificacionInhouse { get; set; }

    }

    public class ReporteResumenMontosTotalizadoDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
       
        public decimal IngresoVentas { get; set; }
        public decimal PagoEnFechaVenc { get; set; }

    }
    public class ReporteResumenMontosDesagregadoDTO
	{
		public string PeriodoPorFechaVencimiento { get; set; }
		public List<ReportePendientePeriodoDesagregadoDetalleDTO> Detalle { get; set; }
	}
	public class ReporteResumenMontosDesagregadoDetalleDTO
	{
		public decimal Proyectado { get; set; }
		public decimal Actual { get; set; }
		public decimal Diferencia { get; set; }
		public decimal DiferenciaRetirosCD { get; set; }
		public decimal DiferenciaRetirosSD { get; set; }
		public decimal MontoPagadoProy { get; set; }
		public decimal MontoPagado { get; set; }
		public decimal MontoPagadoVentas { get; set; }
		public decimal MontoRecuperadoMes { get; set; }
		public decimal ProyectadoVentas { get; set; }
		public decimal ModificacionesSinRetiro { get; set; }
	}

    public class ReporteResumenMontosDetalleFinalDTO {
        public string Anterior { get; set; }
        public string Actual { get; set; }
        public string TipoMonto { get; set; }//programa
        public string Periodo { get; set; }//periodoporfechavencimiento
        public string Monto { get; set; }//total dolares
    }

    public class ReporteResumenMontosDetalleFinalPorCoordinadorDTO
    {
        public string Anterior { get; set; }
        public string Actual { get; set; }
        public string TipoMonto { get; set; }//programa
        public string Coordinador { get; set; }//coordinador
        public string Periodo { get; set; }//periodoporfechavencimiento
        public string Monto { get; set; }//total dolares
    }

    public class ReporteResumenMontosCambiosDTO {
            public string IdMatricula { get; set; }
            public int NroCuota { get; set; }
            public int NroSubCuota { get; set; }
            public decimal MontoProyectado { get; set; }
            public decimal MontoActual { get; set; }
            public decimal TipoCambio { get; set; }
            public string PeriodoProyectado { get; set; }
            public string PeriodoActual { get; set; }
            public decimal Diferencia { get; set; }
            public string Cambio { get; set; }
       
    }

    public class ReporteResumenMontosCambiosPorCoordinadorDTO
    {
        public string IdMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal MontoProyectado { get; set; }
        public decimal MontoActual { get; set; }
        public decimal TipoCambio { get; set; }
        public string PeriodoProyectado { get; set; }
        public string PeriodoActual { get; set; }
        public decimal Diferencia { get; set; }
        public string Cambio { get; set; }
        public string Coordinador { get; set; }

    }

    public class ReporteResumenMontosDiferenciasDTO
    {
        public string IdMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal MontoProyectado { get; set; }
        public decimal MontoActual { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal PruebaCuota { get; set; }
        public int Version { get; set; }
        public string PeriodoaProyectado { get; set; }
        public string PruebaFechaVencimiento { get; set; }
        public string PeriodoActual { get; set; }
        public decimal Diferencia { get; set; }
        public string DescripcionCambio { get; set; }
        public string DetalleCambio { get; set; }
    }
    public class ReporteResumenMontosDetalleDTO
    {

        public string Tipo { get; set; }
        public List<ReporteResumenMontosDetallesMesesDTO> ListaMontosMeses { get; set; }
    }

    public class ReporteResumenMontosDetallePorCoordinadorDTO
    {

        public string Tipo { get; set; }
        public string Coordinador { get; set; }
        public List<ReportePendienteDetallesMesesDTO> ListaMontosMeses { get; set; }
    }

    public class ReporteResumenMontosDetallesMesesDTO
    {
        public string Mes { get; set; }
        public string Monto { get; set; }
        public string Diferencia { get; set; }

    }

    public class ReporteResumenMontosCompuestoDTO
    {
        public List<ReporteResumenMontos> ReporteResumenMontosTodasModalidades { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosConsultorias { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoPeriodoActual { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoPeriodoCierre { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosVariacionMensual { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosNuevosMatriculados { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoPeru { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoColombia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoBolivia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoOtrosPaises { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadPresencialPeru { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadOnlinePeru { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadAonlinePeru { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadInHousePeru { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadPresencialColombia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadOnlineColombia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadAonlineColombia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadInHouseColombia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadPresencialBolivia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadOnlineBolivia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadAonlineBolivia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadInHouseBolivia { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadPresencialOtrosPaises { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadOnlineOtrosPaises { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadAonlineOtrosPaises { get; set; }
        public List<ReporteResumenMontos> ReporteResumenMontosTotalizadoModalidadInHouseOtrosPaises { get; set; }
    }

    public class ReporteResumenMontos
    {
        public string g { get; set; }
        public List<ReporteResumenMontosDetalleFinalDTO> l { get; set; }
    }

    public class CompuestoReporteResumenMontosDTO
    {
        public List<ReporteResumenMontos> ListaPeriodoActual { get; set; }
        public string Usuario { get; set; }
    }


    public class ReporteResumenMontosPorCoordinador
    {
        public string g { get; set; }        
        public List<ReportePendienteDetalleFinalPorCoordinadorDTO> l { get; set; }
    }
    public class ReporteResumenMontosGeneralTotalDTO
    {
        public List<ReporteResumenMontosPagosDTO> ResumenMontos { get; set; }
        public List<ReporteResumenMontosCierreDTO> ResumenMontosCierre { get; set; }
        public List<ReporteResumenMontosCambiosPorPaisesDTO> Cambios { get; set; }
        public List<ReporteResumenMontosDiferenciasPorPaisesDTO> Diferencias { get; set; }
        //public List<ReporteResumenMontosVariacionesDTO> Variaciones { get; set; }
    }

    public class CombosResumenMontosDTO
    {
        public List<ModalidadCursoFiltroDTO> ListaModalidades { get; set; }
        public List<FiltroDTO> ListaPeriodo { get; set; }
        public List<FiltroDTO> ListaPais { get; set; }


    }

    /*Reporte Resumen Montos Cierre*/

    public class ReporteResumenMontosCierreDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }        
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }
    }

    public class ReporteResumenMontosCambiosPorPaisesDTO
    {
        public int IdMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal MontoProyectado { get; set; }
        public decimal MontoActual { get; set; }
        public decimal TipoCambio { get; set; }
        public string PeriodoProyectado { get; set; }
        public string PeriodoActual { get; set; }
        public decimal Diferencia { get; set; }
        public string Cambio { get; set; }
        public int IdTroncalPais { get; set; }
        public int IdTipoModalidad { get; set; }
    }

    public class ReporteResumenMontosDiferenciasPorPaisesDTO
    {
        public int IdMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal MontoProyectado { get; set; }
        public decimal MontoActual { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal PruebaCuota { get; set; }
        public int Version { get; set; }
        public string PeriodoaProyectado { get; set; }
        public string PruebaFechaVencimiento { get; set; }
        public string PeriodoActual { get; set; }
        public decimal Diferencia { get; set; }
        public string DescripcionCambio { get; set; }
        public string DetalleCambio { get; set; }
    }

    public class ReporteResumenMontosVariacionesDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public decimal ActualMontos { get; set; }
        public decimal ActualCierre { get; set; }
        public decimal MontoPagadoMontos { get; set; }
        public decimal MontoPagadoCierre { get; set; }
        public decimal DiferenciaPorModificacion { get; set; }
        public int NuevaConsultoria { get; set; }
        public decimal NuevasMatriculas { get; set; }
        public decimal IngresoRealNuevasMatriculas { get; set; }
        public decimal PendientMesOrdenServicio { get; set; }
        public decimal PendientMesSinOrdenServicio { get; set; }
        public decimal RetirosCD_Mes { get; set; }
        public decimal RetirosSD_Mes { get; set; }
        public decimal IncrementosDisminucionesCronograma { get; set; }
        public decimal ModificacionInhouse { get; set; }
    }

    public class ReporteResumenMontosCierrePeriodoDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }

    }

    public class ReporteResumenMontosUnionCierreDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }

    }

    public class FiltroCierreResumenMontosDTO
    {
        public int IdPeriodo { get; set; }
        public string Usuario { get; set; }
    }

    public class FiltroCierrePorDiaDTO
    {
        public DateTime FechaCierre { get; set; }
        public string Usuario { get; set; }
    }

    public class ResultadoCierreResumenMontosDTO
    {
        public int? ErrorNumber { get; set; }
        public int? ErrorSeverity { get; set; }
        public int? ErrorState { get; set; }
        public string ErrorProcedure { get; set; }
        public int? ErrorLine { get; set; }
        public string ErrorMessage { get; set; }
    }
}
