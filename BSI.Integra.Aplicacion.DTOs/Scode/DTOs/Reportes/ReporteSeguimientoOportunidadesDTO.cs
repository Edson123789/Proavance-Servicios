using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteSeguimientoOportunidadesDTO
    {
        public int? Id { get; set; }
        public string Area { get; set; }
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
        public decimal? ProbabilidadActualValor { get; set; }
        public string probabilidadActual { get; set; }
        public decimal? ProbabilidadNuevoValor { get; set; }
        public string probabilidadNuevo { get; set; }
        public string CodigoPago { get; set; }
        public string Sentinel { get; set; }
        public string NombreGrupo { get; set; }
        public double? MontoTotal { get; set; }
        public double? MontoTotalDolares { get; set; }
        public string Moneda { get; set; }
        public double? TotalPago { get; set; }
        public double? MontoPagado { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdEstadoActividadDetalle { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string Verificado { get; set; }
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public string Paquete { get; set; }
        public double? Matricula { get; set; }
        public double? MatriculaDolares { get; set; }
        //public string Contrato { get; set; }
        public int? IdMatriculaObservacion { get; set; }
        public int? IdCriterioCalificacion { get; set; }
        public string Descuento { get; set; }
        public string NombreCampania { get; set; }
        public string FacebookNombreAnuncio { get; set; }
        public decimal? Probabilidad1 { get; set; }
        public decimal? Probabilidad2 { get; set; }
        public string Clasificacion1 { get; set; }
        public string Clasificacion2 { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int? IdBusqueda { get; set; }
        public string Interaccion { get; set; }
        public string UrlOrigen { get; set; }

        public string GrabacionIntegra { get; set; }
        public string GrabacionCentral { get; set; }
        public string ConvenioFirmado { get; set; }
        public string PersonaEncargada { get; set; }
        public string Webphone { get; set; }

        public bool? AccesoTemporal { get; set; }
        public string ProgramaAccesoTemporal { get; set; }
        public string FechaInicioAccesoTemporal { get; set; }
        public string FechaFinAccesoTemporal { get; set; }

        public string CoordinadoraAcademica { get; set; }

        //visualizacion
        public string AsesorSolicitante { get; set; }
        public Nullable<int> IdPersonalSolicitante { get; set; }
        public Nullable<int> IdSolicitudVisualizacion { get; set; }
    }

    public class ReporteSeguimientoOportunidadesModeloDTO
    {
        public int? Id { get; set; }
        public string Area { get; set; }
        public DateTime? FechaCreacion { get; set; }
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
        public string NombreGrupo { get; set; }
        public DateTime? FechaReal { get; set; }
        public string EstadoOcurrencia { get; set; }
        public DateTime? FechaCierre { get; set; }
        public decimal? ProbabilidadActualValor { get; set; }
        public string ProbabilidadActual { get; set; }
        public decimal? ProbabilidadNuevoValor { get; set; }
        public string ProbabilidadNuevo { get; set; }

    }
}
