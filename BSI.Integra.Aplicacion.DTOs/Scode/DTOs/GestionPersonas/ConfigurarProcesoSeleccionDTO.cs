using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfigurarProcesoSeleccionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int IdSede { get; set; }
        public string Sede { get; set; }
        public string Codigo { get; set; }
        public string Url { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaInicioProceso { get; set; }
        public DateTime? FechaFinProceso { get; set; }
    }
    public class EstructuraBasicaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? EsCalificadoPorPostulante { get; set; }
    }
    public class ExamenAsignadoProcesoDTO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdExamen { get; set; }
        public int NroOrden { get; set; }
        public string Nombre { get; set; }
    }

    public class EvaluacionAsignadoProcesoDTO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdEvaluacion { get; set; }
        public int NroOrden { get; set; }
        public string Nombre { get; set; }
        public bool? EsCalificadoPorPostulante { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
    }
    public class ProcesoSeleccionAgrupadoInsertarModificarDTO {
        //public List<ExamenAsignadoProcesoDTO> ListaExamenes { get; set; }
        public List<ExamenAsignadoEtapaDTO> listaEtapas { get; set; }
        public ConfigurarProcesoSeleccionDTO ConfiguracionProcesoSeleccion { get; set; }
        public List<EvaluacionAsignadoProcesoDTO> listaEvaluaciones { get; set; }
        public List<EvaluacionAsignadoProcesoDTO> listaEvaluacionesEvaluador { get; set; }
        public string Usuario { get; set; }
    }

    public class EliminacionConfiguracionProceso{
        public ConfigurarProcesoSeleccionDTO ProcesoSeleccion { get; set; }
        public string Usuario { get; set; }
    }
    public class ExamenAsignadoEtapaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int NroOrden { get; set; }
    }

	public class ComponenteAsociadoEvaluacion
	{
		public int IdExamen { get; set; }
		public string NombreExamen { get; set; }
		public decimal? FactorComponente { get; set; }
		public int IdEvaluacion { get; set; }
	}
    public class EtapaProcesoSeleccionDTO
    {
        public int IdEtapa { get; set; }
        public int IdProcesoSeleccionEtapa { get; set; }
    }

    public class ObtenerConfiguracionExamenTestDTO
    {
        public int IdExamenTest { get; set; }
        public int IdExamen { get; set; }
        public int? IdGrupo { get; set; }
        public int CantidadPreguntas { get; set; }
    }
    public class CentroCostoComponenteDTO
    {
        public int IdExamenTest { get; set; }
        public string ExamenTest { get; set; }
        public int IdExamen { get; set; }
        public string Examen { get; set; }
        public int? IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int? CantidadDiasAcceso { get; set; }
    }
    public class AsociacionCursoComponenteDTO
    {
        public int IdExamenTest { get; set; }
        public int IdExamen { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? CantidadDiasAcceso { get; set; }
        public string Usuario { get; set; }
    }
    public class FiltroExamenTestExamenDTO
    {
        public int IdExamenTest { get; set; }
        public int IdExamen { get; set; }
        public string Examen { get; set; }
    }
}
