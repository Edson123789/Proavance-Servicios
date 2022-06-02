using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteExamenPostulanteDTO
    {
        public string IdPuesto { get; set; }
        public int? IdGrupoComparacion { get; set; }
        public string IdSede { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Postulante { get; set; }
        public string EstadoFiltro { get; set; }
    }

    public class DatosExamenPostulanteDTO
    {
        public int IdProceso { get; set; }
        public string NombreProceso { get; set; }
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public int Edad { get; set; }
        public int IdSexo { get; set; }
        public int IdExamen { get; set; }
        public string NombreExamen { get; set; }
        public string Titulo { get; set; }
        public int? IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public int? IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public decimal? Puntaje { get; set; }
        public int? IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int? IdEtapa { get; set; }
        public string NombreEtapa { get; set; }
        public decimal? FactorComponente { get; set; }
        public decimal? FactorGrupo { get; set; }
        public decimal? FactorEvaluacion { get; set; }
        public int? IdFormulaComponente { get; set; }
        public int? IdFormulaGrupo { get; set; }
        public int? IdFormulaEvaluacion { get; set; }
        public bool? EstadoAcceso { get; set; }
        public int? IdPespecificoCurso { get; set; }
        public int? CantidadConfigurado { get; set; }
        public int? CantidadResuelto { get; set; }
        public decimal? PuntajeCurso { get; set; }
    }
    public class ProcesoSelecionExamenesCompletosDTO
    {
        public int IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public int Edad { get; set; }
        public string Examen { get; set; }
        public int? IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int IdExamen { get; set; }
        public string Evaluacion { get; set; }
        public int? IdEvaluacion { get; set; }
        public string Grupo { get; set; }
        public int? IdGrupo { get; set; }
        public int? IdEtapa { get; set; }
        public string Etapa { get; set; }
        public string NotaAprobatoria { get; set; }
        public string Simbolo { get; set; }
        public string Registro { get; set; }
        public int Orden { get; set; }
        public bool EsAprobado { get; set; }
        public bool CalificaPorCentil { get; set; }
        public int? IdSexo { get; set; }
        public decimal? OrdenReal { get; set; }
        public int? IdFormulaGrupo { get; set; }
        public bool? EstadoAcceso { get; set; }
        public bool? ConfiguracionComponenteCurso { get; set; }
        public int? IdExamenAccesoTemporal { get; set; }
        public int? CantidadConfigurado { get; set; }
        public int? CantidadResuelto { get; set; }
        public decimal? PuntajeCurso { get; set; }
    }
    public class NotaPostulanteDTO
    {
        public int IdProceso { get; set; }
        public string ProcesoSeleccion { get; set; }
        public int IdSexo { get; set; }
        public int IdExamen { get; set; }
        public string NombreExamen { get; set; }
        public int? IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public int? IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public decimal? Puntaje { get; set; }
        public int? IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int? IdEtapa { get; set; }
        public string NombreEtapa { get; set; }
        public decimal? FactorComponente { get; set; }
        public decimal? FactorGrupo { get; set; }
        public decimal? FactorEvaluacion { get; set; }
        public int? IdFormulaComponente { get; set; }
        public int? IdFormulaGrupo { get; set; }
        public int? IdFormulaEvaluacion { get; set; }
        public bool? EstadoAcceso { get; set; }
        public int? CantidadConfigurado { get; set; }
        public int? CantidadResuelto { get; set; }
        public decimal? PuntajeCurso { get; set; }
    }

    public class DatosNotaPorPostulanteDTO
    {
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public string NombreProceso { get; set; }
        public List<NotaPostulanteDTO> ListaNotas {get;set;}
    }
    public class EvaluacionPostulanteDTO
    {
        public int? IdEvaluacion { get; set; }
        public List<NotaPostulanteDTO> ListaComponentesEvaluacion { get; set; }
    }


	public class ReportePruebaDTO
	{
		public int IdPostulante { get; set; }
		public string Postulante { get; set; }
		public List<ReportePruebaDetalleDTO> Etapas { get; set; }

	}
	public class ReportePruebaDetalleDTO
	{
		public int IdProcesoSeleccion { get; set; }
		public string ProcesoSeleccion { get; set; }
		public int? IdEtapa { get; set; }
		public string Etapa { get; set; }
		public int EstadoEtapa { get; set; }
        public int IdEstadoEtapaProceso { get; set; }
        public bool EtapaContactado { get; set; }
        public int? NroOrden { get; set; }
        public bool? EsCalificadoPorPostulante { get; set; }
    }
    public class DatosEvaluacionAprobadaDTO
    {
        public int IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public int? IdEtapa { get; set; }
        public string Etapa { get; set; }
        public int? IdEvaluacion { get; set; }
        public decimal? ContadorAprobado { get; set; }
        public decimal? ContadorTotal { get; set; }
        public decimal? dividido { get; set; }
        public bool EsAprobado { get; set; }
        public int IdEstadoEtapa { get; set; }
    }


    public class CalificacionEtapaDTO
    {
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int? IdEtapa { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public bool? EtapaContactado { get; set; }
        public bool? EsEtapaActual { get; set; }
        public int? OrdenEtapa { get; set; }
    }

    public class CalificacionEtapaConsolidadoDTO
    {
        public int? IdPostulante { get; set; }
        public List<CalificacionEtapaDTO> listaEtapaCalificada { get; set; }
        public string Usuario { get; set; }
    }
    public class ProcesoSelecionExamenesCompletosComplementoDTO
    {
        public int IdProcesoSeleccion { get; set; }
        public int IdExamen { get; set; }
        public int? IdEvaluacion { get; set; }
        public int? IdGrupo { get; set; }
        public int? IdEtapa { get; set; }
        public int Orden { get; set; }
        public bool EsAprobado { get; set; }
        public bool CalificaPorCentil { get; set; }
        public decimal? OrdenReal { get; set; }
    }

    public class ObtenerTipoExamenDTO
    {
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int? IdEtapa { get; set; }
        public string Usuario { get; set; }
    }
    public class TipoEvaluacionDTO
    {
        public int TipoEvaluacion { get; set; }
        public int? IdEvaluacion { get; set; }
    }

    public class AgrupacionDetalleEvaluacionDTO
    {
        public List<ProcesoSelecionExamenesCompletosDTO> Agrupado { get; set; }
        public List<ProcesoSelecionExamenesCompletosDTO> Detalle { get; set; }
    }

    public class CantidadEvaluacionesConfiguradasDTO
    {
        public int IdExamen { get; set; }
        public int Cantidad { get; set; }
    }
}
