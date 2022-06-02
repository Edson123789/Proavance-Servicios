using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: EncuestaFinalNuevaAulaController
	/// Autor: Lourdes Priscila.
	/// Fecha: 16/06/2021
	/// <summary>
	/// Controlador para graficar el reporte de la encuesta final del nuevo aula virtual
	/// </summary>
    [Route("api/EncuestaFinalNuevaAula")]
    [ApiController]
    public class EncuestaFinalNuevaAulaController : ControllerBase
    {		
		private readonly integraDBContext _integraDBContext;

		public EncuestaFinalNuevaAulaController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 17/06/2021
		/// Version: 1.0
		/// <summary>
		/// Funcion que trae los datos para el reporte de encuesta final
		/// </summary>
		/// <param name="Data">Datos del filtro que vienen de interfaz</param>
		/// <returns>Retorma una lista del tipo PEspecificoFiltroPGeneralDTO </returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ReporteEncuestaFinal([FromBody] FiltroReporteEncuestaFinalNuevaAulaDTO Data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<ReporteEncuestaFinalNuevaAulaDTO> reporte = new List<ReporteEncuestaFinalNuevaAulaDTO>();
				ReporteEncuestaFinalNuevaAulaRepositorio _repEncuestaFinalNuevaAulaRepositorio = new ReporteEncuestaFinalNuevaAulaRepositorio();
				var datos = _repEncuestaFinalNuevaAulaRepositorio.ObtenerDatosEncuestaFinal(Data);
				var idMatriculas = datos.Select(x => x.IdMatriculaCabecera).Distinct().ToList(); 
				foreach (var id in idMatriculas)
                {
					var respuestaAlumno = datos.Where(x=> x.IdMatriculaCabecera== id).ToList();
					ReporteEncuestaFinalNuevaAulaDTO datosReporte = new ReporteEncuestaFinalNuevaAulaDTO();
					
					var rpta1 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 736);
					var rpta2 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 737);
					var rpta3 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 738);
					var rpta4 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 739);
					var rpta5 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 740);
					var rpta6 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 741);
					var rpta7 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 742);
					var rpta8 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 743);
					var rpta9 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 744);
					var rpta10 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 745);
					var rpta11 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 746);
					var rpta12 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 747);
					var rpta13 = datos.Where(x => x.IdMatriculaCabecera == id && x.IdPregunta == 748).ToList();
					List<string> respuestas13 = new List<string>();
					foreach (var r in rpta13)
					{
						
						respuestas13.Add(r.Respuesta);
					}
					var rpta14 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 749);
					var rpta15 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 750);
					var rpta16 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 751);
					var rpta17 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 752);
					var rpta18 = datos.FirstOrDefault(x => x.IdMatriculaCabecera == id && x.IdPregunta == 753);
					datosReporte.ProgramaGeneral = rpta1.ProgramaGeneral;
					datosReporte.ProgramaEspecifico = rpta1.ProgramaEspecifico;
					datosReporte.Alumno = rpta1.Alumno;
					datosReporte.Fecha = rpta1.Fecha;
					datosReporte.Docente = rpta1.Docente;
					datosReporte.CodigoMatricula = rpta1.CodigoMatricula;
					datosReporte.Pregunta1ServicioAtencionCoordinador = rpta1.Respuesta;
					datosReporte.Pregunta2PrecisionInformacion = rpta2.Respuesta;
					datosReporte.Pregunta3TiempoRespuestaConsultas = rpta3.Respuesta;
					datosReporte.Pregunta4CapacidadRespuestaSolicitud = rpta4.Respuesta;
					datosReporte.Pregunta5MecanismoEvaluacionCurso = rpta5.Respuesta;
					datosReporte.Pregunta6ForoConsultas = rpta6.Respuesta;
					datosReporte.Pregunta7NivelDificultadAutoevaluacion = rpta7.Respuesta;
					datosReporte.Pregunta8NivelExigenciaTarea = rpta8.Respuesta;
					datosReporte.Pregunta9PlataformaDigitalFacilitaCurso = rpta9.Respuesta;
					datosReporte.Pregunta10CalidadVideos = rpta10.Respuesta;
					datosReporte.Pregunta11MaterialAudiovisual = rpta11.Respuesta;
					datosReporte.Pregunta12MaterialesCurso = rpta12.Respuesta;
					datosReporte.Pregunta13FactoresMotivacionMatricula = respuestas13;
					datosReporte.Pregunta14FactorDecisivoMatricula = rpta14.Respuesta;
					datosReporte.Pregunta15FortalezaCurso = rpta15.Respuesta;
					datosReporte.Pregunta16DebilidadCurso = rpta16.Respuesta;
					datosReporte.Pregunta17RecomendacionPrograma = rpta17.Respuesta;
					datosReporte.Pregunta18VolverInscripcion = rpta18.Respuesta;
					reporte.Add(datosReporte);

				}
				
				return Ok(reporte);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: GET
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 17/06/2021
		/// Version: 1.0
		/// <summary>
		/// Función que nos trae los datos para llenar los combos de la interfaz
		/// </summary>
		/// <returns>Retorma una lista del tipo PEspecificoFiltroPGeneralDTO </returns>
		[Route("[Action]")]
		[HttpGet]
		public ActionResult LlenarCombosReporteEncuestaFinal()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
				ProveedorRepositorio repProveedorRep = new ProveedorRepositorio();
				var Detalles = new
				{
					//detallecategoria = repCriterioE.ObtenerModalidadPorId(IdCriterioEvaluacion),
					ProgramaGeneral = _repPGeneral.ObtenerProgramasFiltro(),
					Docente = repProveedorRep.ObtenerNombreProveedorParaHonorario(),
				};
				return Ok(Detalles);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: Post
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 17/06/2021
		/// Version: 1.0
		/// <summary>
		/// Funcion que nos trae el nombre y el id de los programas especificos segun el IdPgeneral
		/// </summary>
		/// <param name="IdPgeneral">Id del Programa General</param>
		/// <returns>Retorma una lista del tipo PEspecificoFiltroPGeneralDTO </returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult LlenarCombosPepecificoPorProgramaGeneral(FiltroPgeneral IdPgeneral)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
				List<PEspecificoFiltroPGeneralDTO> listaPespecifico = new List<PEspecificoFiltroPGeneralDTO>();
				foreach(var p in IdPgeneral.Pgeneral)
                {
					var lista = _repPEspecifico.ObtenerProgramaEspecificoPorIdPGeneral(p);
					listaPespecifico.AddRange(lista);
				}
				return Ok(listaPespecifico);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
