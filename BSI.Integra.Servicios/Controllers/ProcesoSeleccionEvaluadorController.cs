using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: ProcesoSeleccionEvaluadorController
	/// Autor: Britsel Calluchi, Luis Huallpa, Edgar S.
	/// Fecha: 03/03/2021
	/// <summary>
	/// Gestión de evaluación de evaluadores
	/// </summary>
	[Route("api/ProcesoSeleccionEvaluador")]

	public class ProcesoSeleccionEvaluadorController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly ExamenAsignadoEvaluadorRepositorio _repExamenAsignadoEvaluador;
		private readonly ExamenRealizadoRespuestaEvaluadorRepositorio _repExamenRealizadoRespuestaEvaluador;
		private readonly PuestoTrabajoRepositorio _repPuestoTrabajo;
		private readonly SedeTrabajoRepositorio _repSedeTrabajo;
		private readonly ProcesoSeleccionRepositorio _repProcesoSeleccion;
		private readonly PersonalRepositorio _repPersonal;

		public ProcesoSeleccionEvaluadorController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repExamenAsignadoEvaluador = new ExamenAsignadoEvaluadorRepositorio(_integraDBContext);
			_repExamenRealizadoRespuestaEvaluador = new ExamenRealizadoRespuestaEvaluadorRepositorio(_integraDBContext);
			_repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
			_repSedeTrabajo = new SedeTrabajoRepositorio(_integraDBContext);
			_repProcesoSeleccion = new ProcesoSeleccionRepositorio();
			_repPersonal = new PersonalRepositorio(_integraDBContext);
		}

		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi, Luis Huallpa, Edgar S.
		/// Fecha: 03/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Combos para Módulo
		/// </summary>
		/// <returns> Objeto Agrupado: List<FiltroIdNombreDTO>, List<FiltroDTO>, List<FiltroDTO>, List<PersonalAutocompleteDTO> </returns>
		[HttpPost]
		[Route("[action]")]
		public ActionResult ObtenerCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var comboPuestoTrabajo = _repPuestoTrabajo.GetFiltroIdNombre();
				var comboSedeTrabajo = _repSedeTrabajo.ObtenerTodoFiltro();
				var procesoSeleccion = _repProcesoSeleccion.ObtenerProcesoSeleccionParaCombo();
				var personal = _repPersonal.CargarPersonalParaFiltro();

				return Ok(new { PuestoTrabajo = comboPuestoTrabajo, SedeTrabajo = comboSedeTrabajo, ProcesoSeleccion = procesoSeleccion, Personal = personal });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi, Luis Huallpa, Edgar S.
		/// Fecha: 03/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Evaluaciones de Evaluador por Filtro
		/// </summary>
		/// <returns> Lista de ObjetoDTO : List<EvaluadorEvaluacionDTO> </returns>
		[HttpPost]
		[Route("[action]")]
		public ActionResult ObtenerEvaluacionesEvaluador([FromBody]EvaluadorEvaluacionFiltroDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaEvaluacionEvaluador = _repExamenAsignadoEvaluador.ObtenerListaEvaluacionEvaluador(Filtro);
				return Ok(listaEvaluacionEvaluador);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi, Luis Huallpa, Edgar S.
		/// Fecha: 03/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Preguntas Respuestas Test de Evaluador
		/// </summary>
		/// <returns> Lista Agrupada de ObjetoDTO : List<PreguntaTestDTO> </returns>
		[HttpPost]
		[Route("[action]")]
		public ActionResult ObtenerPreguntasRespuestasTestEvaluador([FromBody]TestInformacionDTO Test)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaTest = _repExamenAsignadoEvaluador.ObtenerPreguntasTest(Test);
				var agrupado = listaTest.GroupBy(x => new { x.IdEvaluacion, x.IdProcesoSeleccion, x.IdPostulante }).Select(g => new PreguntaTestAgrupadoDTO
				{
					IdEvaluacion = g.Key.IdEvaluacion,
					IdProcesoSeleccion = g.Key.IdProcesoSeleccion,
					IdPostulante = g.Key.IdPostulante,
					ListaPreguntas = g.GroupBy(y => new { y.IdExamenAsignado, y.IdExamen, y.IdPregunta, y.EnunciadoPregunta, y.NroOrdenPregunta, y.IdPreguntaTipo, y.PreguntaTipo, y.IdTipoRespuesta, y.TipoRespuesta }).Select(h => new PreguntaTestAgrupadoDetalleDTO
					{
						IdExamenAsignado = h.Key.IdExamenAsignado,
						IdExamen = h.Key.IdExamen,
						IdPregunta = h.Key.IdPregunta,
						EnunciadoPregunta = h.Key.EnunciadoPregunta,
						NroOrdenPregunta = h.Key.NroOrdenPregunta,
						IdPreguntaTipo = h.Key.IdPreguntaTipo,
						PreguntaTipo = h.Key.PreguntaTipo,
						IdTipoRespuesta = h.Key.IdTipoRespuesta,
						TipoRespuesta = h.Key.TipoRespuesta
					}).OrderBy(x => x.NroOrdenPregunta).ToList()
				}).FirstOrDefault();
				foreach (var item in agrupado.ListaPreguntas)
				{
					item.ListaRespuestas = _repExamenAsignadoEvaluador.ObtenerListaPreguntasRespuestaTest(item.IdExamen, item.IdPregunta);
				}
				return Ok(agrupado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi, Luis Huallpa, Edgar S.
		/// Fecha: 03/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Preguntas y Respuestas Realizadas de Test Evaluador
		/// </summary>
		/// <returns> Lista Agrupada de ObjetoDTO : List<PreguntaTestDTO> </returns>
		[HttpPost]
		[Route("[action]")]
		public ActionResult ObtenerPreguntasRespuestasRealizadasTestEvaluador([FromBody]TestInformacionDTO Test)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaTest = _repExamenAsignadoEvaluador.ObtenerPreguntasTest(Test);
				var agrupado = listaTest.GroupBy(x => new { x.IdEvaluacion, x.IdProcesoSeleccion, x.IdPostulante }).Select(g => new PreguntaTestAgrupadoDTO
				{
					IdEvaluacion = g.Key.IdEvaluacion,
					IdProcesoSeleccion = g.Key.IdProcesoSeleccion,
					IdPostulante = g.Key.IdPostulante,
					ListaPreguntas = g.GroupBy(y => new { y.IdExamenAsignado, y.IdExamen, y.IdPregunta, y.EnunciadoPregunta, y.NroOrdenPregunta, y.IdPreguntaTipo, y.PreguntaTipo, y.IdTipoRespuesta, y.TipoRespuesta }).Select(h => new PreguntaTestAgrupadoDetalleDTO
					{
						IdExamenAsignado = h.Key.IdExamenAsignado,
						IdExamen = h.Key.IdExamen,
						IdPregunta = h.Key.IdPregunta,
						EnunciadoPregunta = h.Key.EnunciadoPregunta,
						NroOrdenPregunta = h.Key.NroOrdenPregunta,
						IdPreguntaTipo = h.Key.IdPreguntaTipo,
						PreguntaTipo = h.Key.PreguntaTipo,
						IdTipoRespuesta = h.Key.IdTipoRespuesta,
						TipoRespuesta = h.Key.TipoRespuesta
					}).OrderBy(x => x.NroOrdenPregunta).ToList()
				}).FirstOrDefault();
				foreach (var item in agrupado.ListaPreguntas)
				{
					item.ListaRespuestas = _repExamenAsignadoEvaluador.ObtenerListaPreguntasRespuestaTest(item.IdExamen, item.IdPregunta);
					item.ListaRespuestasRealizada = _repExamenRealizadoRespuestaEvaluador.GetBy(x => x.IdExamenAsignadoEvaluador == item.IdExamenAsignado && x.IdPregunta == item.IdPregunta).Select(x => new RespuestaRealizadaDTO
					{
						Id = x.Id,
						IdExamenAsignadoEvaluador = x.IdExamenAsignadoEvaluador,
						IdPregunta = x.IdPregunta,
						IdRespuestaPregunta = x.IdRespuestaPregunta,
						TextoRespuesta = x.TextoRespuesta
					}).ToList();
				}
				return Ok(agrupado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi, Luis Huallpa, Edgar S.
		/// Fecha: 03/03/2021
		/// Versión: 2.0
		/// <summary>
		/// Guarda respuestas realizadas por evaluador y califica etapa de evaluación
		/// </summary>
		/// <returns> Confirmación de inserción : Bool </returns>
		[HttpPost]
		[Route("[action]")]
		public ActionResult EnviarRespuestasTest([FromBody]RespuestaEvaluacionEvaluadorDTO RespuestaTest)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionAsignacionEvaluacionRepositorio _repConfiguracionAsignacionEvaluacion = new ConfiguracionAsignacionEvaluacionRepositorio(_integraDBContext);
				EtapaProcesoSeleccionCalificadoRepositorio _repEtapaProcesoSeleccionCalificado = new EtapaProcesoSeleccionCalificadoRepositorio(_integraDBContext);
				ExamenRepositorio _repExamen = new ExamenRepositorio(_integraDBContext);
				ExamenAsignadoEvaluadorRepositorio _repExamenAsignadoEvaluador = new ExamenAsignadoEvaluadorRepositorio(_integraDBContext);
				var examen = _repExamen.GetBy(x=> x.Id ==RespuestaTest.IdExamenEvaluacionEvaluador).FirstOrDefault();
				var configuracion = _repConfiguracionAsignacionEvaluacion.GetBy(x => x.IdProcesoSeleccion == RespuestaTest.IdProcesoSeleccionEvaluacionEvaluador && x.IdEvaluacion == examen.IdExamenTest).FirstOrDefault();
				var etapaAnterior = _repEtapaProcesoSeleccionCalificado.GetBy(x => x.IdPostulante == RespuestaTest.IdPostulanteEvaluacionEvaluador && x.EsEtapaActual == true).FirstOrDefault();
				if(etapaAnterior != null)
				{
					etapaAnterior.EsEtapaActual = false;
					etapaAnterior.UsuarioModificacion = RespuestaTest.Usuario;
					etapaAnterior.FechaModificacion = DateTime.Now;
					_repEtapaProcesoSeleccionCalificado.Update(etapaAnterior);
				}
				var etapaCalificada = _repEtapaProcesoSeleccionCalificado.GetBy(x => x.IdPostulante == RespuestaTest.IdPostulanteEvaluacionEvaluador && x.IdProcesoSeleccionEtapa == configuracion.IdProcesoSeleccionEtapa).FirstOrDefault();
				if (etapaCalificada != null)
                {
					var idEtapaCalificadaActual = etapaCalificada.Id;

					if (RespuestaTest.IdEstadoEvaluacionEvaluador == 2 || RespuestaTest.IdEstadoEvaluacionEvaluador == 3 || RespuestaTest.IdEstadoEvaluacionEvaluador == 4 || RespuestaTest.IdEstadoEvaluacionEvaluador == 9) etapaCalificada.EsEtapaAprobada = false;
					else etapaCalificada.EsEtapaAprobada = true;
                    if (RespuestaTest.IdEstadoEvaluacionEvaluador == 9) // ESTADO Sin Rendir
                    {
						etapaCalificada.EsEtapaActual = false;
						etapaCalificada.EsContactado = false;
					}
					else
					{
						etapaCalificada.EsEtapaActual = true;
						etapaCalificada.EsContactado = true;
					}
					etapaCalificada.IdEstadoEtapaProcesoSeleccion = RespuestaTest.IdEstadoEvaluacionEvaluador;
					etapaCalificada.UsuarioModificacion = RespuestaTest.Usuario;
					etapaCalificada.FechaModificacion = DateTime.Now;

					_repEtapaProcesoSeleccionCalificado.Update(etapaCalificada);

                    if (etapaCalificada.EsEtapaAprobada)
                    {
						//Configurar la siguiente evaluación en estado "En Proceso" 
						var etapaOrden = _repEtapaProcesoSeleccionCalificado.ObtenerListaEtapaExamenesPorPostulante(RespuestaTest.IdProcesoSeleccionEvaluacionEvaluador, RespuestaTest.IdPostulanteEvaluacionEvaluador);
						var ordenActual = etapaOrden.Where(x => x.Id == idEtapaCalificadaActual).OrderByDescending(x => x.NroOrden).FirstOrDefault();
						var ordenPosterior = etapaOrden.Where(x => x.NroOrden == ordenActual.NroOrden + 1).FirstOrDefault();
                        if (ordenPosterior != null)
                        {
							var actualizarEtapaPosterior = _repEtapaProcesoSeleccionCalificado.GetBy(x => x.Id == ordenPosterior.Id).FirstOrDefault();
							if (actualizarEtapaPosterior != null)
							{
								actualizarEtapaPosterior.EsEtapaActual = true;
								actualizarEtapaPosterior.EsContactado = true;
								actualizarEtapaPosterior.IdEstadoEtapaProcesoSeleccion = 3; // En Proceso
								actualizarEtapaPosterior.UsuarioModificacion = RespuestaTest.Usuario;
								actualizarEtapaPosterior.FechaModificacion = DateTime.Now;

								_repEtapaProcesoSeleccionCalificado.Update(actualizarEtapaPosterior);
							}
						}
					}
				}

                if (RespuestaTest.IdEstadoEvaluacionEvaluador != 3 && RespuestaTest.IdEstadoEvaluacionEvaluador != 4 && RespuestaTest.IdEstadoEvaluacionEvaluador != 9) //En caso de abandono o manualmente colocar la opción sin Rendir o en proceso no se registra las respuestas
                {
					var examenRealizado = _repExamenAsignadoEvaluador.GetBy(x => x.IdPostulante == RespuestaTest.IdPostulanteEvaluacionEvaluador && x.IdExamen == examen.Id).FirstOrDefault();
					if (examenRealizado.EstadoExamen == false)
					{
						var agrupado = RespuestaTest.ListaRespuestasEvaluador.GroupBy(x => x.idexamenasignado).Select(x => new RespuestaTestDTO
						{
							IdExamenAsignado = x.Key,
							ListaRespuestas = x.GroupBy(y => new { y.idexamen, y.idpregunta, y.idrespuesta, y.textorespuesta, y.flag }).Select(y => new RespuestaTestAgrupadaDTO
							{
								IdExamen = y.Key.idexamen,
								IdPregunta = y.Key.idpregunta,
								IdRespuesta = y.Key.idrespuesta,
								TextoRespuesta = y.Key.textorespuesta,
								Flag = y.Key.flag
							}).ToList()
						}).ToList();

						foreach (var item in agrupado)
						{
							var registro = _repExamenAsignadoEvaluador.FirstById(item.IdExamenAsignado);
							foreach (var respuesta in item.ListaRespuestas)
							{
								if (respuesta.Flag)
								{
									ExamenRealizadoRespuestaEvaluadorBO examenRealizadoRespuesta = new ExamenRealizadoRespuestaEvaluadorBO()
									{
										IdExamenAsignadoEvaluador = item.IdExamenAsignado,
										IdPregunta = respuesta.IdPregunta,
										IdRespuestaPregunta = respuesta.IdRespuesta,
										TextoRespuesta = respuesta.TextoRespuesta,
										Estado = true,
										UsuarioCreacion = RespuestaTest.Usuario,
										UsuarioModificacion = RespuestaTest.Usuario,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now
									};
									var res = _repExamenRealizadoRespuestaEvaluador.Insert(examenRealizadoRespuesta);
								}
							}
							registro.EstadoExamen = true;
							registro.UsuarioModificacion = RespuestaTest.Usuario;
							registro.FechaModificacion = DateTime.Now;
							_repExamenAsignadoEvaluador.Update(registro);
						}
					}
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi, Luis Huallpa, Edgar S.
		/// Fecha: 03/03/2021
		/// Versión: 2.0
		/// <summary>
		/// Obtiene Evaluaciones asignadas a evaluador por Filtro
		/// </summary>
		/// <returns> Lista de Objeto DTO : List<EvaluacionesAsignadasEvaluador> </returns>
		[HttpPost]
		[Route("[action]")]
		public ActionResult ObtenerEvaluacionesAsignadasEvaluador([FromBody]FiltroEvaluacionEvaluador Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaEvaluacionEvaluador = _repExamenAsignadoEvaluador.ObtenerListaEvaluacionEvaluador(Filtro);
				return Ok(listaEvaluacionEvaluador);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}