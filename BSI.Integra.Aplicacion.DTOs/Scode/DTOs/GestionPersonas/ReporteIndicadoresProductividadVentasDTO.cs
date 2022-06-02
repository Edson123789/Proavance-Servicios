using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteIndicadoresProductividadVentasDTO
    {
        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public TimeSpan? Lunes1 { get; set; }
        public TimeSpan? Lunes2 { get; set; }
        public TimeSpan? Lunes3 { get; set; }
        public TimeSpan? Lunes4 { get; set; }
        public TimeSpan? Martes1 { get; set; }
        public TimeSpan? Martes2 { get; set; }
        public TimeSpan? Martes3 { get; set; }
        public TimeSpan? Martes4 { get; set; }
        public TimeSpan? Miercoles1 { get; set; }
        public TimeSpan? Miercoles2 { get; set; }
        public TimeSpan? Miercoles3 { get; set; }
        public TimeSpan? Miercoles4 { get; set; }
        public TimeSpan? Jueves1 { get; set; }
        public TimeSpan? Jueves2 { get; set; }
        public TimeSpan? Jueves3 { get; set; }
        public TimeSpan? Jueves4 { get; set; }
        public TimeSpan? Viernes1 { get; set; }
        public TimeSpan? Viernes2 { get; set; }
        public TimeSpan? Viernes3 { get; set; }
        public TimeSpan? Viernes4 { get; set; }
        public TimeSpan? Sabado1 { get; set; }
        public TimeSpan? Sabado2 { get; set; }
        public TimeSpan? Sabado3 { get; set; }
        public TimeSpan? Sabado4 { get; set; }
        public TimeSpan? Domingo1 { get; set; }
        public TimeSpan? Domingo2 { get; set; }
        public TimeSpan? Domingo3 { get; set; }
        public TimeSpan? Domingo4 { get; set; }
        public string Usuario { get; set; }

    }

    public class ReporteProductividadVentasHorasTrabajadasDTO
    {
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string CoordinadorAsesor { get; set; }
        public string NombreJefe { get; set; }
        public DateTime FechaPeriodo { get; set; }
        public string PeriodoMarcacion { get; set; }

        public int HorasTrabajadas { get; set; }
        public int DiasTrabajados { get; set; }
        public decimal TotalVendido { get; set; }
        public string Sede { get; set; }
    }

    public class ReporteProductividadVentasIndicadoresDTO
    {
        public string PeriodoMarcacion { get; set; }

        public int DiasLaborables { get; set; }
        public decimal TotalVendido { get; set; }
        public int DiasCoordinador { get; set; }
        public int DiasAsesor { get; set; }
        public int NumeroIS { get; set; }
        public decimal PagoDolaresRecargasTelefonicas { get; set; }
        public decimal PagoDolaresTelefonosFijos { get; set; }
        public decimal PagoDolaresCapacitacionPersonal { get; set; }
        public decimal PagoDolaresTelefonosMoviles { get; set; }
        public decimal PagoDolaresPublicidad { get; set; }
        public decimal PagoDolaresSueldoAsesores { get; set; }
        public decimal PagoDolaresSueldoCoordinadores { get; set; }
        public decimal PagoDolaresGratificacionAsesores { get; set; }
        public decimal PagoDolaresGratificacionCoordinadores { get; set; }
        public decimal PagoDolaresCTSAsesores { get; set; }
        public decimal PagoDolaresCTSCoordinadores { get; set; }
        public decimal PagoDolaresParticipacionesAsesores { get; set; }
        public decimal PagoDolaresParticipacionesCoordinadores { get; set; }
        public decimal PagoDolaresSisPensionarioAsesores { get; set; }
        public decimal PagoDolaresSisPensionarioCoordinadores { get; set; }
        public decimal PagoDolaresEsSaludAsesores { get; set; }
        public decimal PagoDolaresEsSaludCoordinadores { get; set; }
        public decimal PagoDolaresRenta5Asesores { get; set; }
        public decimal PagoDolaresRenta5Coordinadores { get; set; }
        public decimal PagoDolaresComisiones { get; set; }
        public decimal PagoDolaresVacaciones { get; set; }
        public decimal PagoDolaresPremios { get; set; }
        public decimal PagoDolaresSueldoLiquidacion { get; set; }
        public decimal PagoDolaresGratificacionLiquidacion { get; set; }
        public decimal PagoDolaresCTSLiquidacion { get; set; }
        public int NumeroCoordinadores { get; set; }
        public int NumeroAsesores { get; set; }
        public decimal BeaticosVentas { get; set; }
    }
    public class ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO
    {
        public string Periodo { get; set; }
        public List<ReporteProductividadVentasHorasTrabajadasDTO> DetalleFecha { get; set; }
    }

    public class ReporteIndicadoresProductividadIndicadoresAgrupadoDTO
    {
        public string Periodo { get; set; }
        public List<ReporteProductividadVentasIndicadoresDTO> DetalleFecha { get; set; }
    }

    public class ReporteIndicadoresProductividadVentasCompuestoDTO
    {
        public List<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> ReporteProductividadHorasTrabajadas { get; set; }
        public List<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> ReporteProductividadToTalVendido { get; set; }
        public List<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> ReporteHorasTrabajadasProductividadEquipo { get; set; }
        public List<ReporteProductividadVentas> ReporteIndicadoresProductividad { get; set; }

    }

    public class ReporteIndicadoresProductividadVentasGeneralDTO
    {
        public List<ReporteProductividadVentasHorasTrabajadasDTO> HorasTrabajadas { get; set; }
        public List<ReporteProductividadVentasIndicadoresDTO> Indicadores { get; set; }
    }

    //Obtener el formato requerido en detalles


    public class ReporteProductividadVentasDetallesMesesDTO
    {
        public string Mes { get; set; }
        public string Monto { get; set; }
        public string Diferencia { get; set; }

    }

    public class ReporteProductividadVentasDetalleDTO
    {

        public string Tipo { get; set; }
        public List<ReporteProductividadVentasDetallesMesesDTO> ListaMontosMeses { get; set; }
    }

    public class ReporteProductividadVentasDetalleFinalDTO
    {
        public string Anterior { get; set; }
        public string Actual { get; set; }
        public string TipoMonto { get; set; }//programa
        public string Periodo { get; set; }//periodoporfechavencimiento
        public string Monto { get; set; }//total dolares
    }

    public class ReporteProductividadVentas
    {
        public string g { get; set; }
        public List<ReporteProductividadVentasDetalleFinalDTO> l { get; set; }
    }
}
