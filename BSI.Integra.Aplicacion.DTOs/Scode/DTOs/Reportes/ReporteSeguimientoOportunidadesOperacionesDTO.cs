using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteSeguimientoOportunidadesOperacionesDTO
    {
        public int? Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdFaseOportunidadIP { get; set; }
        public int? IdFaseOportunidadPF { get; set; }
        public int? IdFaseoportunidadIC { get; set; }
        public string CodigoPagoIC { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public string UsuarioCreacion { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public string Asesor { get; set; }
        public string CodigoFaseOportunidad { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public string NombrePais { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string probabilidadActual { get; set; }
        public string CodigoPago { get; set; }
        public string Sentinel { get; set; }
        public string NombreGrupo { get; set; }
        public double? MontoTotal { get; set; }
        public string Moneda { get; set; }
        public double? TotalPago { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdEstadoActividadDetalle { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string Verificado { get; set; }
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public int? DiasAtrasoCuotaPago { get; set; }
        public int? CuotasAtrasoCuotaPago { get; set; }
        public decimal? MontoAtrasoCuotaPago { get; set; }
        public string MonedaCuotaPago { get; set; }
        public string EstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public string AgendaTab { get; set; }
        public string DiasSeguimientoActividadesEjecutadas { get; set; }
        public string UltimoContacto { get; set; }
        public string DiasSeguimientoActividadesEjecutadas7dias { get; set; }
        public string DiasSeguimientoActividadesEjecutadas14dias { get; set; }
        public string DiasSeguimientoActividadesEjecutadas30dias { get; set; }
        public int? NroCuotasAtrasadas { get; set; }
        public decimal? MontoCuotasAtrasadas { get; set; }
        public string EstadoAcademico { get; set; }
        public string Paquete { get; set; }
        public string GrabacionCentral { get; set; }
        public string ConvenioFirmado { get; set; }
        public string PersonaEncargada { get; set; }
        public int IdCriterioCalificacion { get; set; }
        public int IdMatriculaObservacion { get; set; }
        public int? IdPEspecifico { get; set; }
        public int? ValorAvanceReal { get; set; }
        public string AvanceReal { get; set; }
        public string AvanceRealSesion { get; set; }
        public string AvanceRealAutoevaluacion { get; set; }
        public string AvanceProgramado { get; set; }
        public string AvanceProgramadoSesion { get; set; }
        public string AvanceProgramadoAutoevaluacion { get; set; }
        public int? ValorAvanceProgramado { get; set; }
        public int? ReproduccionVideoReal { get; set; }
        public int? ReproduccionVideoProgramado { get; set; }
        public int? CumplimientoAvance { get; set; }
        public int? DiasDesdeUltimoAvance { get; set; }
        public bool? AccesoTemporal { get; set; }
        public string ProgramaAccesoTemporal { get; set; }
        public string FechaInicioAccesoTemporal { get; set; }
        public string FechaFinAccesoTemporal { get; set; }
        public string AulaVirtual { get; set; }
        public string FechaFinalizacion { get; set; }
    }
}
