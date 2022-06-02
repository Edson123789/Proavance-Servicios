using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using System.Transactions;
using Google.Api.Ads.Common.Util;
using CsvHelper;
using System.IO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: Operaciones/PreguntaProgramaCapacitacion
    /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
    /// Fecha: 20/02/2021
    /// <summary>
    /// Gestiona las acciones con las preguntas de un programa capacitacion
    /// </summary>
	[Route("api/MaestroPreguntaProgramaCapacitacion")]
	public class MaestroPreguntaProgramaCapacitacionController : ControllerBase
	{

		private readonly integraDBContext _integraDBContext;
		private readonly PreguntaProgramaCapacitacionRepositorio _repPreguntaProgramaCapacitacion;
		private readonly PreguntaTipoRepositorio _repPreguntaTipo;
		private readonly RespuestaPreguntaProgramaCapacitacionRepositorio _repRespuestaPreguntaProgramaCapacitacion;
		private readonly PgeneralRepositorio _repPGeneral;
		private readonly PreguntaIntentoRepositorio _repPreguntaIntento;
		private readonly PreguntaIntentoDetalleRepositorio _repPreguntaIntentoDetalle;
		private readonly ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio;
		private readonly TipoMarcadorRepositorio _repTipoMarcador;
		private readonly PespecificoRepositorio _repPEspecifico;
		public MaestroPreguntaProgramaCapacitacionController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
			_repPreguntaProgramaCapacitacion = new PreguntaProgramaCapacitacionRepositorio(_integraDBContext);
			_repPreguntaTipo = new PreguntaTipoRepositorio(_integraDBContext);
			_repRespuestaPreguntaProgramaCapacitacion = new RespuestaPreguntaProgramaCapacitacionRepositorio(_integraDBContext);
			_repPGeneral = new PgeneralRepositorio(_integraDBContext);
			_repPreguntaIntento = new PreguntaIntentoRepositorio(_integraDBContext);
			_repPreguntaIntentoDetalle = new PreguntaIntentoDetalleRepositorio(_integraDBContext);
			_configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio(_integraDBContext);
			_repTipoMarcador = new TipoMarcadorRepositorio(_integraDBContext);
			_repPEspecifico = new PespecificoRepositorio(_integraDBContext);
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Obtiene los combos necesarios para el funcionamiento del modulo
        /// </summary>
        /// <returns>Objeto anonimo { PGeneral, TipoMarcador, ProgramaEspecifico }</returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var preguntaTipo = _repPreguntaTipo.ObtenerPreguntaTipo();
				var pgeneral = _repPGeneral.ObtenerProgramasFiltro();
				var tipoRespuestaCalificacion = _repRespuestaPreguntaProgramaCapacitacion.ObtenerTipoRespuestaCalificacion();
				var tipoMarcador = _repTipoMarcador.ObtenerTipoMarcador();
				var pespecifico = _repPEspecifico.ObtenerPEspecificoProgramaGeneral();
				return Ok(new { PreguntaTipo = preguntaTipo, PGeneral = pgeneral, TipoRespuestaCalificacion = tipoRespuestaCalificacion, TipoMarcador = tipoMarcador, ProgramaEspecifico = pespecifico });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 20/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Funcion para obtener la lista de capitulos y sus sesiones respectivas con relacion al programa general
        /// </summary>
		/// <param name="IdPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Booleano con respuesta 200 y la lista de objeto (CapituloSesionProgramaCapacitacionDTO) o 400 con el mensaje de error</returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCapituloSesionesPGeneral([FromBody] int IdPGeneral)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<CapituloSesionProgramaCapacitacionDTO> listaRegistro;
				if (IdPGeneral == -1)
				{
					listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();
				}
				else
				{
					var respuesta = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoPrograma(IdPGeneral);
					var listadoEstructura = (from x in respuesta
											  group x by x.NumeroFila into newGroup
											  select newGroup).ToList();

					List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();

					foreach (var item in listadoEstructura)
					{
						EstructuraCapituloProgramaBO objeto = new EstructuraCapituloProgramaBO();
						objeto.OrdenFila = item.Key;
						foreach (var itemRegistros in item)
						{
							switch (itemRegistros.NombreTitulo)
							{
								case "Capitulo":
									objeto.Nombre = itemRegistros.Nombre;
									objeto.Capitulo = itemRegistros.Contenido;
									objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
									objeto.IdPgeneral = itemRegistros.IdPgeneral;
									break;
								case "Sesion":
									objeto.Sesion = itemRegistros.Contenido;
									objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
									objeto.IdDocumentoSeccionPw = itemRegistros.Id;
									break;
								case "SubSeccion":
									objeto.SubSesion = itemRegistros.Contenido;
									objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
									if (!string.IsNullOrEmpty(objeto.SubSesion))
									{
										objeto.IdDocumentoSeccionPw = itemRegistros.Id;
									}
									break;
								default:
									objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
									objeto.TotalSegundos = itemRegistros.TotalSegundos;
									break;
							}
						}
						lista.Add(objeto);
					}

					var rpta = lista.OrderBy(x => x.OrdenFila).ToList();

					var listas = rpta.GroupBy(x => new { x.IdPgeneral, x.Nombre, x.Capitulo, x.OrdenCapitulo });

					listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();

					foreach (var capitulo in listas)
					{
						CapituloSesionProgramaCapacitacionDTO registro = new CapituloSesionProgramaCapacitacionDTO();
						registro.IdPGeneral = capitulo.Key.IdPgeneral;
						registro.CapituloProgramaCapacitacion = capitulo.Key.Capitulo;
						registro.IdCapituloProgramaCapacitacion = capitulo.Key.OrdenCapitulo;

						registro.ListaSesionesProgramaCapacitacion = new List<SesionSubSeccionProgramaCapacitacionDTO>();

						registro.ListaSesionesProgramaCapacitacion = capitulo.GroupBy(x => x.Sesion).Select(x => new SesionSubSeccionProgramaCapacitacionDTO {
							SesionProgramaCapacitacion = x.Key,
							IdSesionProgramaCapacitacion = capitulo.GroupBy(z => new { z.OrdenFila, z.Sesion, z.SubSesion }).Where(z => z.Key.Sesion == x.Key).FirstOrDefault().Key.OrdenFila,
							ListaSubSeccionProgramaCapacitacion = capitulo.GroupBy(y => new { y.OrdenFila, y.Sesion, y.SubSesion }).Where(y => y.Key.Sesion == x.Key).Select(y => new SubSeccionProgramaCapacitacionDTO { 
								IdSesionProgramaCapacitacion = y.Key.OrdenFila,
								SubSeccionProgramaCapacitacion = y.Key.SubSesion
							}).ToList()
						}).ToList();

						listaRegistro.Add(registro);
					}
				}
				return Ok(listaRegistro);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Obtiene las preguntas
        /// </summary>
        /// <returns>Lista de objetos (PreguntaProgramaCapacitacionRegistradaDTO) con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerPreguntas()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var preguntas = _repPreguntaProgramaCapacitacion.ObtenerPreguntasRegistradas();
				return Ok(preguntas);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]/{IdPgeneral}/{GrupoPregunta}")]
		[HttpPost]
		public ActionResult ObtenerlistaPreguntasPorGrupo(int IdPgeneral, string GrupoPregunta)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<ListadoPreguntaPorEstructuraBO> res;
				if (IdPgeneral > 0 && !string.IsNullOrEmpty(GrupoPregunta))
				{
					res = _repRespuestaPreguntaProgramaCapacitacion.ObtenerlistaPreguntasPorGrupo(IdPgeneral, GrupoPregunta);
					return Ok(res);
				}
				else
				{
					res = new List<ListadoPreguntaPorEstructuraBO>();
					return Ok(res);
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Obtiene las respuestas de una pregunta segun un id especifico
        /// </summary>
		/// <param name="IdPreguntaProgramaCapacitacion">Id de la pregunta de programa capacitacion(PK de la tabla ope.T_PreguntaProgramaCapacitacion)</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerRespuestasPregunta([FromBody]int IdPreguntaProgramaCapacitacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<RespuestaPreguntaProgramaCapacitacionDTO> res;
				if (IdPreguntaProgramaCapacitacion > 0)
				{
					res = _repRespuestaPreguntaProgramaCapacitacion.ObtenerRespuestaPreguntaProgramaCapacitacion(IdPreguntaProgramaCapacitacion);
					return Ok(res);
				}
				else
				{
					res = new List<RespuestaPreguntaProgramaCapacitacionDTO>();
					return Ok(res);
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Obtiene los intentos que tiene la configuracion por pregunta
        /// </summary>
		/// <param name="IdPreguntaIntento">Id del intento de pregunta(PK de la tabla gp.T_PreguntaIntento)</param>
        /// <returns>Lista de objeto de tipo (PreguntaIntentoDetalleDTO) con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerIntentosPregunta([FromBody]int IdPreguntaIntento)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<PreguntaIntentoDetalleDTO> res;
				if (IdPreguntaIntento > 0)
				{
					res = _repPreguntaIntentoDetalle.ObtenerIntentosPreguntaRegistrados(IdPreguntaIntento);
					return Ok(res);
				}
				else
				{
					res = new List<PreguntaIntentoDetalleDTO>();
					return Ok(res);
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[Route("[action]/{IdPgeneral}/{GrupoPregunta}")]
		[HttpPost]
		public ActionResult ObtenerlistaGrupoPreguntasConRespuestas(int IdPgeneral, string GrupoPregunta)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<ListadoPreguntaPorEstructuraBO> res;
				List<ListadoGrupoPreguntaSesion> grupoPreguntas = new List<ListadoGrupoPreguntaSesion>();
				if (IdPgeneral > 0 && !string.IsNullOrEmpty(GrupoPregunta))
				{
					res = _repRespuestaPreguntaProgramaCapacitacion.ObtenerlistaPreguntasPorGrupo(IdPgeneral, GrupoPregunta);


                    if (res.Count > 0)
                    {
                        foreach (var pregunta in res)
                        {
							ListadoGrupoPreguntaSesion _pregunta = new ListadoGrupoPreguntaSesion() {
								Id = pregunta.Id,
								IdPgeneral = pregunta.IdPgeneral,
								OrdenFilaCapitulo = pregunta.OrdenFilaCapitulo,
								OrdenFilaSesion = pregunta.OrdenFilaSesion,
								EnunciadoPregunta = pregunta.EnunciadoPregunta,
								RespuestaAleatoria = pregunta.RespuestaAleatoria,
								MostrarFeedbackInmediato = pregunta.MostrarFeedbackInmediato,
								MostrarFeedbackPorPregunta = pregunta.MostrarFeedbackPorPregunta,
								NumeroMaximoIntento = pregunta.NumeroMaximoIntento,
								TipoRespuesta = pregunta.TipoRespuesta
							};
							var _respuestas = _repRespuestaPreguntaProgramaCapacitacion.ObtenerRespuestaPreguntaProgramaCapacitacion(pregunta.Id);
                            if (_respuestas != null && _respuestas.Count > 0)
                            {
								_pregunta.Respuestas = new List<ListadoGrupoSesionRespuestas>();
                                foreach (var respuesta in _respuestas)
                                {
									var plainTextBytesPositivo = System.Text.Encoding.UTF8.GetBytes(respuesta.FeedbackPositivo);
									var plainTextBytesNegativo = System.Text.Encoding.UTF8.GetBytes(respuesta.FeedbackNegativo);

									ListadoGrupoSesionRespuestas _respuesta = new ListadoGrupoSesionRespuestas() { 
										Id=respuesta.Id,
										IdPreguntaProgramaCapacitacion=respuesta.IdPreguntaProgramaCapacitacion,
										RespuestaCorrecta=respuesta.RespuestaCorrecta,
										NroOrden=respuesta.NroOrden,
										EnunciadoRespuesta=respuesta.EnunciadoRespuesta,
										Puntaje=respuesta.Puntaje,
										FeedbackPositivo = System.Convert.ToBase64String(plainTextBytesPositivo),
										FeedbackNegativo = System.Convert.ToBase64String(plainTextBytesNegativo)
									};
									_pregunta.Respuestas.Add(_respuesta);
								}

							}
							else
                            {
								_pregunta.Respuestas = null;

							}
							grupoPreguntas.Add(_pregunta);
						}
                    }

					return Ok(grupoPreguntas);
				}
				else
				{
					res = new List<ListadoPreguntaPorEstructuraBO>();
					return Ok(res);
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]/{IdPgeneral}")]
		[HttpPost]
		public ActionResult ObtenerListaPreguntasPorSeccionNroGrupo(int IdPgeneral)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<listaNumeroGruposSesionBO> res;
				if (IdPgeneral > 0)
				{
					res = _repRespuestaPreguntaProgramaCapacitacion.ObtenerListaPreguntasPorSeccionNroGrupo(IdPgeneral);

					return Ok(res);
				}
				else
				{
					res = new List<listaNumeroGruposSesionBO>();
					return Ok(res);
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Inserta una pregunta especifica
        /// </summary>
		/// <param name="Filtro">Objeto de tipo CompuestoCrucigramaProgramaCapacitacionDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarPregunta([FromBody]CompuestoPreguntaProgramaCapacitacionDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					PreguntaIntentoBO preguntaIntento = new PreguntaIntentoBO()
					{
						NumeroMaximoIntento = Filtro.PreguntaIntento.NumeroMaximoIntento,
						ActivarFeedbackMaximoIntento = Filtro.PreguntaIntento.ActivarFeedbackMaximoIntento,
						MensajeFeedback = Filtro.PreguntaIntento.MensajeFeedbackIntento,
						Estado = true,
						UsuarioCreacion = Filtro.Usuario,
						UsuarioModificacion = Filtro.Usuario,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now
					};
					_repPreguntaIntento.Insert(preguntaIntento);

					foreach(var item in Filtro.PreguntaIntento.PreguntaIntentoDetalle)
					{
						PreguntaIntentoDetalleBO preguntaIntentoDetalle = new PreguntaIntentoDetalleBO
						{
							IdPreguntaIntento = preguntaIntento.Id,
							PorcentajeCalificacion = item.PorcentajeCalificacion,
							Estado = true,
							UsuarioCreacion = Filtro.Usuario,
							UsuarioModificacion = Filtro.Usuario,
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now
						};
						_repPreguntaIntentoDetalle.Insert(preguntaIntentoDetalle);
					}

					PreguntaProgramaCapacitacionBO pregunta = new PreguntaProgramaCapacitacionBO()
					{
						IdTipoRespuesta = Filtro.Pregunta.IdTipoRespuesta,
						EnunciadoPregunta = Filtro.Pregunta.Enunciado,
						MinutosPorPregunta = Filtro.Pregunta.MinutosPorPregunta,
						RespuestaAleatoria = Filtro.Pregunta.RespuestaAleatoria,
						ActivarFeedBackRespuestaCorrecta = Filtro.Pregunta.ActivarFeedBackRespuestaCorrecta,
						ActivarFeedBackRespuestaIncorrecta = Filtro.Pregunta.ActivarFeedBackRespuestaIncorrecta,
						MostrarFeedbackInmediato = Filtro.Pregunta.MostrarFeedbackInmediato,
						MostrarFeedbackPorPregunta = Filtro.Pregunta.MostrarFeedbackPorPregunta,
						Estado = true,
						UsuarioCreacion = Filtro.Usuario,
						UsuarioModificacion = Filtro.Usuario,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now,
						IdPreguntaIntento = preguntaIntento.Id,
						IdPreguntaTipo = Filtro.Pregunta.IdPreguntaTipo,
						IdTipoRespuestaCalificacion = Filtro.Pregunta.IdTipoRespuestaCalificacion,
						FactorRespuesta = Filtro.Pregunta.FactorRespuesta,
						IdPgeneral = Filtro.Pregunta.IdPGeneral,
						IdPespecifico = Filtro.Pregunta.IdPEspecifico,
						OrdenFilaCapitulo = Filtro.Pregunta.IdCapitulo,
						OrdenFilaSesion = Filtro.Pregunta.IdSesion,
						GrupoPregunta = Filtro.Pregunta.GrupoPregunta,
						IdTipoMarcador = Filtro.Pregunta.IdTipoMarcador,
						ValorMarcador = Filtro.Pregunta.ValorMarcador,
						OrdenPreguntaGrupo = Filtro.Pregunta.OrdenPreguntaGrupo						
					};
					_repPreguntaProgramaCapacitacion.Insert(pregunta);

					foreach (var item in Filtro.RespuestaPregunta)
					{
						int? puntajeTipoRespuesta = null;
						if (Filtro.Pregunta.IdTipoRespuestaCalificacion.HasValue)
						{
							int tipoRes = Filtro.Pregunta.IdTipoRespuestaCalificacion.Value;
							if (tipoRes == 1) //Directo
							{
								puntajeTipoRespuesta = item.Puntaje;
							}
							else if (tipoRes == 2) //Inversa
							{
								if (Filtro.Pregunta.FactorRespuesta.HasValue)
								{
									int factorRes = Filtro.Pregunta.FactorRespuesta.Value;
									puntajeTipoRespuesta = factorRes - item.Puntaje;
								}
							}
							else //negativo
							{
								if (Filtro.Pregunta.FactorRespuesta.HasValue)
								{
									int factorRes = Filtro.Pregunta.FactorRespuesta.Value;
									if (!item.RespuestaCorrecta)
										puntajeTipoRespuesta = item.Puntaje - factorRes;
									else
										puntajeTipoRespuesta = item.Puntaje;
								}
							}
						}
						RespuestaPreguntaProgramaCapacitacionBO respuestaPregunta = new RespuestaPreguntaProgramaCapacitacionBO()
						{
							IdPreguntaProgramaCapacitacion = pregunta.Id,
							RespuestaCorrecta = item.RespuestaCorrecta,
							NroOrden = item.NroOrden,
							EnunciadoRespuesta = item.EnunciadoRespuesta,
							NroOrdenRespuesta = item.NroOrdenRespuesta,
							Puntaje = item.Puntaje,
							FeedbackPositivo = item.FeedbackPositivo,
							FeedbackNegativo = item.FeedbackNegativo,
							Estado = true,
							UsuarioCreacion = Filtro.Usuario,
							UsuarioModificacion = Filtro.Usuario,
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
							PuntajeTipoRespuesta = puntajeTipoRespuesta
						};
						_repRespuestaPreguntaProgramaCapacitacion.Insert(respuestaPregunta);
					}
					scope.Complete();
				}

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Actualiza una pregunta en especifico
        /// </summary>
		/// <param name="Filtro">Objeto de tipo CompuestoPreguntaProgramaCapacitacionDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarPregunta([FromBody]CompuestoPreguntaProgramaCapacitacionDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					PreguntaIntentoBO preguntaIntento;
					var pregunta = _repPreguntaProgramaCapacitacion.FirstById(Filtro.Pregunta.Id);
					if (pregunta.IdPreguntaIntento.HasValue)
					{
						preguntaIntento = _repPreguntaIntento.FirstById(pregunta.IdPreguntaIntento.Value);
						preguntaIntento.NumeroMaximoIntento = Filtro.PreguntaIntento.NumeroMaximoIntento;
						preguntaIntento.ActivarFeedbackMaximoIntento = Filtro.PreguntaIntento.ActivarFeedbackMaximoIntento;
						preguntaIntento.MensajeFeedback = Filtro.PreguntaIntento.MensajeFeedbackIntento;
						preguntaIntento.UsuarioModificacion = Filtro.Usuario;
						preguntaIntento.FechaModificacion = DateTime.Now;
						_repPreguntaIntento.Update(preguntaIntento);

						var listaPreguntaIntento = _repPreguntaIntentoDetalle.GetBy(x => x.IdPreguntaIntento == preguntaIntento.Id);
						foreach (var item in listaPreguntaIntento)
						{
							if (!Filtro.PreguntaIntento.PreguntaIntentoDetalle.Any(x => x.Id == item.Id))
							{
								_repPreguntaIntentoDetalle.Delete(item.Id, Filtro.Usuario);
							}
						}

						foreach (var item in Filtro.PreguntaIntento.PreguntaIntentoDetalle)
						{
							PreguntaIntentoDetalleBO preguntaIntentoDetalle;
							if (item.Id > 0)
							{
								preguntaIntentoDetalle = _repPreguntaIntentoDetalle.FirstById(item.Id);
								preguntaIntentoDetalle.PorcentajeCalificacion = item.PorcentajeCalificacion;
								preguntaIntentoDetalle.UsuarioModificacion = Filtro.Usuario;
								preguntaIntentoDetalle.FechaModificacion = DateTime.Now;
								_repPreguntaIntentoDetalle.Update(preguntaIntentoDetalle);
							}
							else
							{
								preguntaIntentoDetalle = new PreguntaIntentoDetalleBO
								{
									IdPreguntaIntento = preguntaIntento.Id,
									PorcentajeCalificacion = item.PorcentajeCalificacion,
									Estado = true,
									UsuarioCreacion = Filtro.Usuario,
									UsuarioModificacion = Filtro.Usuario,
									FechaCreacion = DateTime.Now,
									FechaModificacion = DateTime.Now
								};
								_repPreguntaIntentoDetalle.Insert(preguntaIntentoDetalle);
							}
						}
					}
					else
					{
						preguntaIntento = new PreguntaIntentoBO()
						{
							NumeroMaximoIntento = Filtro.PreguntaIntento.NumeroMaximoIntento,
							ActivarFeedbackMaximoIntento = Filtro.PreguntaIntento.ActivarFeedbackMaximoIntento,
							MensajeFeedback = Filtro.PreguntaIntento.MensajeFeedbackIntento,
							Estado = true,
							UsuarioCreacion = Filtro.Usuario,
							UsuarioModificacion = Filtro.Usuario,
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now
						};
						_repPreguntaIntento.Insert(preguntaIntento);

						foreach (var item in Filtro.PreguntaIntento.PreguntaIntentoDetalle)
						{
							PreguntaIntentoDetalleBO preguntaIntentoDetalle = new PreguntaIntentoDetalleBO
							{
								IdPreguntaIntento = preguntaIntento.Id,
								PorcentajeCalificacion = item.PorcentajeCalificacion,
								Estado = true,
								UsuarioCreacion = Filtro.Usuario,
								UsuarioModificacion = Filtro.Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now
							};
							_repPreguntaIntentoDetalle.Insert(preguntaIntentoDetalle);
						}
						pregunta.IdPreguntaIntento = preguntaIntento.Id;
					}

					pregunta.IdTipoRespuesta = Filtro.Pregunta.IdTipoRespuesta;
					pregunta.EnunciadoPregunta = Filtro.Pregunta.Enunciado;
					pregunta.MinutosPorPregunta = Filtro.Pregunta.MinutosPorPregunta;
					pregunta.RespuestaAleatoria = Filtro.Pregunta.RespuestaAleatoria;
					pregunta.ActivarFeedBackRespuestaCorrecta = Filtro.Pregunta.ActivarFeedBackRespuestaCorrecta;
					pregunta.ActivarFeedBackRespuestaIncorrecta = Filtro.Pregunta.ActivarFeedBackRespuestaIncorrecta;
					pregunta.MostrarFeedbackInmediato = Filtro.Pregunta.MostrarFeedbackInmediato;
					pregunta.MostrarFeedbackPorPregunta = Filtro.Pregunta.MostrarFeedbackPorPregunta;
					pregunta.UsuarioModificacion = Filtro.Usuario;
					pregunta.FechaModificacion = DateTime.Now;
					pregunta.IdTipoRespuestaCalificacion = Filtro.Pregunta.IdTipoRespuestaCalificacion;
					pregunta.FactorRespuesta = Filtro.Pregunta.FactorRespuesta;
					pregunta.IdPreguntaTipo = Filtro.Pregunta.IdPreguntaTipo;
					pregunta.IdPgeneral = Filtro.Pregunta.IdPGeneral;
					pregunta.IdPespecifico = Filtro.Pregunta.IdPEspecifico;
					pregunta.OrdenFilaCapitulo = Filtro.Pregunta.IdCapitulo;
					pregunta.OrdenFilaSesion = Filtro.Pregunta.IdSesion;
					pregunta.GrupoPregunta = Filtro.Pregunta.GrupoPregunta;
					pregunta.IdTipoMarcador = Filtro.Pregunta.IdTipoMarcador;
					pregunta.ValorMarcador = Filtro.Pregunta.ValorMarcador;
					pregunta.OrdenPreguntaGrupo = Filtro.Pregunta.OrdenPreguntaGrupo;
					_repPreguntaProgramaCapacitacion.Update(pregunta);

					var rp = _repRespuestaPreguntaProgramaCapacitacion.GetBy(x => x.IdPreguntaProgramaCapacitacion == pregunta.Id);
					foreach (var item in rp)
					{
						if (!Filtro.RespuestaPregunta.Any(x => x.Id == item.Id))
						{
							_repRespuestaPreguntaProgramaCapacitacion.Delete(item.Id, Filtro.Usuario);
						}
					}

					foreach (var item in Filtro.RespuestaPregunta)
					{
						int? puntajeTipoRespuesta = null;
						if (Filtro.Pregunta.IdTipoRespuestaCalificacion.HasValue)
						{
							int tipoRes = Filtro.Pregunta.IdTipoRespuestaCalificacion.Value;
							if (tipoRes == 1) //Directo
							{
								puntajeTipoRespuesta = item.Puntaje;
							}
							else if (tipoRes == 2) //Inversa
							{
								if (Filtro.Pregunta.FactorRespuesta.HasValue)
								{
									int factorRes = Filtro.Pregunta.FactorRespuesta.Value;
									puntajeTipoRespuesta = factorRes - item.Puntaje;
								}
							}
							else //negativo
							{
								if (Filtro.Pregunta.FactorRespuesta.HasValue)
								{
									int factorRes = Filtro.Pregunta.FactorRespuesta.Value;
									if (!item.RespuestaCorrecta)
										puntajeTipoRespuesta = item.Puntaje - factorRes;
									else
										puntajeTipoRespuesta = item.Puntaje;
								}
							}
						}

						RespuestaPreguntaProgramaCapacitacionBO respuestaPregunta;
						if (item.Id > 0)
						{
							respuestaPregunta = _repRespuestaPreguntaProgramaCapacitacion.FirstById(item.Id);
							respuestaPregunta.RespuestaCorrecta = item.RespuestaCorrecta;
							respuestaPregunta.NroOrden = item.NroOrden;
							respuestaPregunta.EnunciadoRespuesta = item.EnunciadoRespuesta;
							respuestaPregunta.NroOrdenRespuesta = item.NroOrdenRespuesta;
							respuestaPregunta.Puntaje = item.Puntaje;
							respuestaPregunta.FeedbackPositivo = item.FeedbackPositivo;
							respuestaPregunta.FeedbackNegativo = item.FeedbackNegativo;
							respuestaPregunta.UsuarioModificacion = Filtro.Usuario;
							respuestaPregunta.FechaModificacion = DateTime.Now;
							respuestaPregunta.PuntajeTipoRespuesta = puntajeTipoRespuesta;
							_repRespuestaPreguntaProgramaCapacitacion.Update(respuestaPregunta);
						}
						else
						{
							respuestaPregunta = new RespuestaPreguntaProgramaCapacitacionBO()
							{
								IdPreguntaProgramaCapacitacion = pregunta.Id,
								RespuestaCorrecta = item.RespuestaCorrecta,
								NroOrden = item.NroOrden,
								EnunciadoRespuesta = item.EnunciadoRespuesta,
								NroOrdenRespuesta = item.NroOrdenRespuesta,
								Puntaje = item.Puntaje,
								FeedbackPositivo = item.FeedbackPositivo,
								FeedbackNegativo = item.FeedbackNegativo,
								Estado = true,
								UsuarioCreacion = Filtro.Usuario,
								UsuarioModificacion = Filtro.Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
								PuntajeTipoRespuesta = puntajeTipoRespuesta
							};
							_repRespuestaPreguntaProgramaCapacitacion.Insert(respuestaPregunta);
						}
					}

					scope.Complete();
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Elimina una pregunta especifica
        /// </summary>
		/// <param name="Filtro">Objeto de tipo EliminarDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarPregunta([FromBody]EliminarDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					var pregunta = _repPreguntaProgramaCapacitacion.FirstById(Filtro.Id);
					if (pregunta != null)
					{
						var respuestaPregunta = _repRespuestaPreguntaProgramaCapacitacion.GetBy(x => x.IdPreguntaProgramaCapacitacion == pregunta.Id);

						if (pregunta.IdPreguntaIntento.HasValue)
						{
							var listaPreguntaIntentoDetalle = _repPreguntaIntentoDetalle.GetBy(x => x.IdPreguntaIntento == pregunta.IdPreguntaIntento.Value);
							foreach (var item in listaPreguntaIntentoDetalle)
							{
								_repPreguntaIntentoDetalle.Delete(item.Id, Filtro.NombreUsuario);
							}
							_repPreguntaIntento.Delete(pregunta.IdPreguntaIntento.Value, Filtro.NombreUsuario);

						}

						foreach (var item in respuestaPregunta)
						{
							_repRespuestaPreguntaProgramaCapacitacion.Delete(item.Id, Filtro.NombreUsuario);
						}
						_repPreguntaProgramaCapacitacion.Delete(pregunta.Id, Filtro.NombreUsuario);
					}
					scope.Complete();
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Funcion para importar desde un archivo CSV a la base de datos
        /// </summary>
		/// <param name="Files">Objeto de tipo IFormFile con las preguntas</param>
        /// <returns>Booleano con respuesta 200 y la objeto anonimo con las propiedades({ Total, Correctos, Error, Errores }) o 400 con el mensaje de error</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ImportarExcel([FromForm] IFormFile Files)
		{
			CsvFile file = new CsvFile();
			List<string> listaErrores = new List<string>();
			try
			{
				int indexError = 0;
				int indexTotal = 0;
				List<ImportacionPreguntaRespuestaProgramaCapacitacionDTO> ListaExcel = new List<ImportacionPreguntaRespuestaProgramaCapacitacionDTO>();
				StreamReader sw = new StreamReader(Files.OpenReadStream(), System.Text.Encoding.GetEncoding("iso-8859-1"));
				using (var cvs = new CsvReader(sw))
				{
					cvs.Configuration.Delimiter = ";";
					cvs.Configuration.MissingFieldFound = null;
					cvs.Read();
					cvs.ReadHeader();
					while (cvs.Read())
					{
						try
						{
							using (TransactionScope scope = new TransactionScope())
							{
								ImportacionPreguntaRespuestaProgramaCapacitacionDTO preguntaRespuestaProgramaCapacitacion = new ImportacionPreguntaRespuestaProgramaCapacitacionDTO()
								{
									NumeroMaximoIntento = cvs.GetField<int?>("NumeroMaximoIntento"),
									ActivarFeedbackMaximoIntento = cvs.GetField<bool?>("ActivarFeedbackMaximoIntento"),
									MensajeFeedback = cvs.GetField<string>("MensajeFeedback"),
									IdTipoRespuesta = cvs.GetField<int?>("IdTipoRespuesta"),
									EnunciadoPregunta = cvs.GetField<string>("EnunciadoPregunta"),
									MinutosPorPregunta = cvs.GetField<int?>("MinutosPorPregunta"),
									RespuestaAleatoria = cvs.GetField<bool?>("RespuestaAleatoria"),
									ActivarFeedBackRespuestaCorrecta = cvs.GetField<bool?>("ActivarFeedBackRespuestaCorrecta"),
									ActivarFeedBackRespuestaIncorrecta = cvs.GetField<bool?>("ActivarFeedBackRespuestaIncorrecta"),
									MostrarFeedbackInmediato = cvs.GetField<bool?>("MostrarFeedbackInmediato"),
									MostrarFeedbackPorPregunta = cvs.GetField<bool?>("MostrarFeedbackPorPregunta"),
									IdPreguntaTipo = cvs.GetField<int?>("IdPreguntaTipo"),
									IdTipoRespuestaCalificacion = cvs.GetField<int?>("IdTipoRespuestaCalificacion"),
									FactorRespuesta = cvs.GetField<int?>("FactorRespuesta"),
									IdPgeneral = cvs.GetField<int>("IdPGeneral"),
									IdPEspecifico = cvs.GetField<int?>("IdPEspecifico"),
									OrdenFilaCapitulo = cvs.GetField<int?>("OrdenFilaCapitulo"),
									Sesion = cvs.GetField<string>("Sesion"),
									SubSeccion = cvs.GetField<string>("Subsesion"),
									GrupoPregunta = cvs.GetField<string>("GrupoPregunta"),
									IdTipoMarcador = cvs.GetField<int?>("IdTipoMarcador"),
									ValorMarcador = cvs.GetField<decimal?>("ValorMarcador"),
									OrdenPreguntaGrupo = cvs.GetField<int?>("OrdenPreguntaGrupo"),
									//============RESPUESTAS=============================
									RespuestaCorrecta = cvs.GetField<bool?>("RespuestaCorrecta"),
									NroOrden = cvs.GetField<int>("NroOrden"),
									EnunciadoRespuesta = cvs.GetField<string>("EnunciadoRespuesta"),
									NroOrdenRespuesta = cvs.GetField<int?>("NroOrdenRespuesta"),
									Puntaje = cvs.GetField<int?>("Puntaje"),
									FeedbackPositivo = cvs.GetField<string>("FeedbackPositivo"),
									FeedbackNegativo = cvs.GetField<string>("FeedbackNegativo"),
									//==========PREGUNTAINTENTODETALLE=======================
									PorcentajeCalificacion = cvs.GetField<int?>("PorcentajeCalificacion"),
								};
								ListaExcel.Add(preguntaRespuestaProgramaCapacitacion);
								scope.Complete();
							}
						}
						catch (Exception e)
						{
							//indexError++;
							//listaErrores.Add("Error en: " + cvs.GetField<string>("EnunciadoPregunta") + " - " + e.Message);
						}

					}
				}
				var agrupado = ListaExcel.GroupBy(x => new
				{
					x.IdTipoRespuesta,
					x.EnunciadoPregunta,
					x.MinutosPorPregunta,
					x.RespuestaAleatoria,
					x.ActivarFeedBackRespuestaCorrecta,
					x.ActivarFeedBackRespuestaIncorrecta,
					x.MostrarFeedbackInmediato,
					x.MostrarFeedbackPorPregunta,
					x.IdPreguntaTipo,
					x.IdTipoRespuestaCalificacion,
					x.FactorRespuesta,
					x.IdPgeneral,
					x.IdPEspecifico,
					x.OrdenFilaCapitulo,
					x.Sesion,
					x.SubSeccion,
					x.GrupoPregunta,
					x.IdTipoMarcador,
					x.ValorMarcador,
					x.OrdenPreguntaGrupo
				}).Select(x => new PreguntaProgramaCapacitacionExcelCompuestoDTO
				{
					PreguntaProgramaCapacitacion = new PreguntaProgramaCapacitacionAgrupadoDTO
					{
						ActivarFeedBackRespuestaCorrecta = x.Key.ActivarFeedBackRespuestaCorrecta,
						ActivarFeedBackRespuestaIncorrecta = x.Key.ActivarFeedBackRespuestaIncorrecta,
						EnunciadoPregunta = x.Key.EnunciadoPregunta,
						FactorRespuesta = x.Key.FactorRespuesta,
						IdPgeneral = x.Key.IdPgeneral,
						IdPEspecifico = x.Key.IdPEspecifico,
						IdPreguntaTipo = x.Key.IdPreguntaTipo,
						IdTipoRespuesta = x.Key.IdTipoRespuesta,
						IdTipoRespuestaCalificacion = x.Key.IdTipoRespuestaCalificacion,
						MinutosPorPregunta = x.Key.MinutosPorPregunta,
						MostrarFeedbackInmediato = x.Key.MostrarFeedbackInmediato,
						MostrarFeedbackPorPregunta = x.Key.MostrarFeedbackPorPregunta,
						OrdenFilaCapitulo = x.Key.OrdenFilaCapitulo,
						Sesion = x.Key.Sesion,
						SubSeccion = x.Key.SubSeccion,
						GrupoPregunta = x.Key.GrupoPregunta,
						IdTipoMarcador = x.Key.IdTipoMarcador,
						ValorMarcador = x.Key.ValorMarcador,
						OrdenPreguntaGrupo = x.Key.OrdenPreguntaGrupo,
						RespuestaAleatoria = x.Key.RespuestaAleatoria,
						PreguntaIntento = x.GroupBy(y => new { y.ActivarFeedbackMaximoIntento, y.MensajeFeedback, y.NumeroMaximoIntento }).Select(y => new PreguntaIntentoAgrupadoDTO
						{
							ActivarFeedbackMaximoIntento = y.Key.ActivarFeedbackMaximoIntento,
							MensajeFeedback = y.Key.MensajeFeedback,
							NumeroMaximoIntento = y.Key.NumeroMaximoIntento,
							PreguntaIntentoDetalle = y.GroupBy(z => z.PorcentajeCalificacion).Select(z => new PreguntaIntentoDetalleAgrupadoDTO
							{
								PorcentajeCalificacion = z.Key
							}).ToList()
						}).FirstOrDefault(),
						RespuestaPreguntaProgramaCapacitacion = x.GroupBy(z => new { z.RespuestaCorrecta, z.NroOrden, z.EnunciadoRespuesta, z.NroOrdenRespuesta, z.Puntaje, z.FeedbackPositivo, z.FeedbackNegativo }).Select(z => new RespuestaPreguntaProgramaCapacitacionAgrupadoDTO
						{
							EnunciadoRespuesta = z.Key.EnunciadoRespuesta,
							FeedbackPositivo = z.Key.FeedbackPositivo,
							FeedbackNegativo = z.Key.FeedbackNegativo,
							NroOrden = z.Key.NroOrden,
							NroOrdenRespuesta = z.Key.NroOrdenRespuesta,
							Puntaje = z.Key.Puntaje,
							RespuestaCorrecta = z.Key.RespuestaCorrecta
						}).ToList()
					}
				}).ToList();

				foreach(var item in agrupado)
				{
					var listaCompuesta = ObtenerCapituloSesionProgramaCapacitacion(item.PreguntaProgramaCapacitacion.IdPgeneral);
					var listaCapitulos = listaCompuesta.Where(x => x.IdCapituloProgramaCapacitacion == item.PreguntaProgramaCapacitacion.OrdenFilaCapitulo).FirstOrDefault();
					int? ordenFilaSesion = null;
					if (listaCapitulos != null)
					{
						var sesion = listaCapitulos.ListaSesionesProgramaCapacitacion.Where(x => x.SesionProgramaCapacitacion.ToLower().Equals(item.PreguntaProgramaCapacitacion.Sesion.ToLower())).FirstOrDefault();

						int idSesionTemporal = 0;

						if (sesion != null)
						{
							idSesionTemporal = sesion.IdSesionProgramaCapacitacion;
						}

						if (!string.IsNullOrEmpty(item.PreguntaProgramaCapacitacion.SubSeccion))
                        {
							try
							{
								idSesionTemporal = sesion.ListaSubSeccionProgramaCapacitacion.FirstOrDefault(y => y.SubSeccionProgramaCapacitacion.ToLower().Equals(item.PreguntaProgramaCapacitacion.SubSeccion.ToLower())).IdSesionProgramaCapacitacion;
							}
							catch(Exception e)
                            {
								idSesionTemporal = sesion.IdSesionProgramaCapacitacion;
                            }
                        }

						if (idSesionTemporal > 0)
						{
							ordenFilaSesion = idSesionTemporal;
						}
					}
					try
					{
						indexTotal++;
						using (TransactionScope scope = new TransactionScope())
						{
							PreguntaIntentoBO preguntaIntento = new PreguntaIntentoBO()
							{
								ActivarFeedbackMaximoIntento = item.PreguntaProgramaCapacitacion.PreguntaIntento.ActivarFeedbackMaximoIntento,
								NumeroMaximoIntento = item.PreguntaProgramaCapacitacion.PreguntaIntento.NumeroMaximoIntento,
								MensajeFeedback = item.PreguntaProgramaCapacitacion.PreguntaIntento.MensajeFeedback,
								Estado = true,
								UsuarioCreacion = "ImportacionExcelOP",
								UsuarioModificacion = "ImportacionExcelOP",
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now
							};
							_repPreguntaIntento.Insert(preguntaIntento);
							foreach(var pi in item.PreguntaProgramaCapacitacion.PreguntaIntento.PreguntaIntentoDetalle)
							{
								if(pi.PorcentajeCalificacion != null)
								{
									PreguntaIntentoDetalleBO preguntaIntentoDetalle = new PreguntaIntentoDetalleBO
									{
										IdPreguntaIntento = preguntaIntento.Id,
										PorcentajeCalificacion = pi.PorcentajeCalificacion,
										Estado = true,
										UsuarioCreacion = "ImportacionExcelOP",
										UsuarioModificacion = "ImportacionExcelOP",
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now
									};
									_repPreguntaIntentoDetalle.Insert(preguntaIntentoDetalle);
								}
							}
							PreguntaProgramaCapacitacionBO preguntaProgramaCapacitacion = new PreguntaProgramaCapacitacionBO()
							{
								IdTipoRespuesta = item.PreguntaProgramaCapacitacion.IdTipoRespuesta,
								EnunciadoPregunta = item.PreguntaProgramaCapacitacion.EnunciadoPregunta,
								MinutosPorPregunta = item.PreguntaProgramaCapacitacion.MinutosPorPregunta,
								RespuestaAleatoria = item.PreguntaProgramaCapacitacion.RespuestaAleatoria,
								ActivarFeedBackRespuestaCorrecta = item.PreguntaProgramaCapacitacion.ActivarFeedBackRespuestaCorrecta,
								ActivarFeedBackRespuestaIncorrecta = item.PreguntaProgramaCapacitacion.ActivarFeedBackRespuestaIncorrecta,
								MostrarFeedbackInmediato = item.PreguntaProgramaCapacitacion.MostrarFeedbackInmediato,
								MostrarFeedbackPorPregunta = item.PreguntaProgramaCapacitacion.MostrarFeedbackPorPregunta,
								IdPreguntaIntento = preguntaIntento.Id,
								IdPreguntaTipo = item.PreguntaProgramaCapacitacion.IdPreguntaTipo,
								IdTipoRespuestaCalificacion = item.PreguntaProgramaCapacitacion.IdTipoRespuestaCalificacion,
								FactorRespuesta = item.PreguntaProgramaCapacitacion.FactorRespuesta,
								IdPgeneral = item.PreguntaProgramaCapacitacion.IdPgeneral,
								IdPespecifico = item.PreguntaProgramaCapacitacion.IdPEspecifico,
								OrdenFilaCapitulo = item.PreguntaProgramaCapacitacion.OrdenFilaCapitulo,
								OrdenFilaSesion = ordenFilaSesion,
								GrupoPregunta = item.PreguntaProgramaCapacitacion.GrupoPregunta,
								IdTipoMarcador = item.PreguntaProgramaCapacitacion.IdTipoMarcador,
								ValorMarcador = item.PreguntaProgramaCapacitacion.ValorMarcador,
								OrdenPreguntaGrupo = item.PreguntaProgramaCapacitacion.OrdenPreguntaGrupo,
								Estado = true,
								UsuarioCreacion = "ImportacionExcelOP",
								UsuarioModificacion = "ImportacionExcelOP",
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
							};
							_repPreguntaProgramaCapacitacion.Insert(preguntaProgramaCapacitacion);

							foreach (var respuesta in item.PreguntaProgramaCapacitacion.RespuestaPreguntaProgramaCapacitacion)
							{
								int? puntajeTipoRespuesta = null;
								int? puntaje = respuesta.Puntaje;
								bool? respuestaCorrecta = respuesta.RespuestaCorrecta;
								if (preguntaProgramaCapacitacion.IdTipoRespuestaCalificacion.HasValue)
								{
									int tipoRes = preguntaProgramaCapacitacion.IdTipoRespuestaCalificacion.Value;
									if (tipoRes == 1) //Directo
									{
										puntajeTipoRespuesta = puntaje;
									}
									else if (tipoRes == 2) //Inversa
									{
										if (preguntaProgramaCapacitacion.FactorRespuesta.HasValue)
										{
											int factorRes = preguntaProgramaCapacitacion.FactorRespuesta.Value;
											puntajeTipoRespuesta = factorRes - puntaje;
										}
									}
									else //negativo
									{
										if (preguntaProgramaCapacitacion.FactorRespuesta.HasValue)
										{
											int factorRes = preguntaProgramaCapacitacion.FactorRespuesta.Value;
											if (respuestaCorrecta.HasValue)
												if (!respuestaCorrecta.Value)
													puntajeTipoRespuesta = puntaje - factorRes;
												else
													puntajeTipoRespuesta = puntaje;
										}
									}
								}
								RespuestaPreguntaProgramaCapacitacionBO respuestaPregunta = new RespuestaPreguntaProgramaCapacitacionBO()
								{
									IdPreguntaProgramaCapacitacion = preguntaProgramaCapacitacion.Id,
									RespuestaCorrecta = respuesta.RespuestaCorrecta,
									NroOrden = respuesta.NroOrden,
									EnunciadoRespuesta = respuesta.EnunciadoRespuesta,
									NroOrdenRespuesta = respuesta.NroOrdenRespuesta,
									Puntaje = respuesta.Puntaje,
									FeedbackPositivo = respuesta.FeedbackPositivo,
									FeedbackNegativo = respuesta.FeedbackNegativo,
									Estado = true,
									UsuarioCreacion = "ImportacionExcelOP",
									UsuarioModificacion = "ImportacionExcelOP",
									FechaCreacion = DateTime.Now,
									FechaModificacion = DateTime.Now,
									PuntajeTipoRespuesta = puntajeTipoRespuesta
								};
								_repRespuestaPreguntaProgramaCapacitacion.Insert(respuestaPregunta);
							}
							scope.Complete();
						}
					}
					catch (Exception e)
					{
						indexError++;
						listaErrores.Add("Error - " + e.Message);
					}

				}
				return Ok(new { Total = indexTotal, Correctos = (indexTotal - indexError), Error = indexError, Errores = listaErrores });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarRespuestaPorSecuenciaVideo([FromBody]CompuestoPreguntasSecuenciaVideoDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var itemPregunta in Filtro.Preguntas)
                    {
						var _registroPreguntas = _repPreguntaProgramaCapacitacion.GetBy(x => x.IdPgeneral == itemPregunta.IdPgeneral && x.GrupoPregunta == itemPregunta.GrupoPregunta).ToList();

                        foreach (var item in _registroPreguntas)
                        {
							PreguntaIntentoBO preguntaIntento;
							var pregunta = _repPreguntaProgramaCapacitacion.FirstById(item.Id);

							pregunta.IdTipoMarcador = itemPregunta.IdTipoVista;
							pregunta.ValorMarcador = itemPregunta.Segundos;
							pregunta.UsuarioModificacion = Filtro.Usuario;
							pregunta.FechaModificacion = DateTime.Now;

							_repPreguntaProgramaCapacitacion.Update(pregunta);
						}
						
                    }
                    

                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Obtiene los capitulos y sesiones para los programas capacitacciones
        /// </summary>
		/// <param name="IdPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objeto de tipo CapituloSesionProgramaCapacitacionDTO</returns>
		private List<CapituloSesionProgramaCapacitacionDTO> ObtenerCapituloSesionProgramaCapacitacion(int IdPGeneral)
		{
			try
			{
				List<CapituloSesionProgramaCapacitacionDTO> listaRegistro;
				if (IdPGeneral == -1)
				{
					listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();
				}
				else
				{
					var respuesta = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoPrograma(IdPGeneral);
					var listadoEstructura = (from x in respuesta
											  group x by x.NumeroFila into newGroup
											  select newGroup).ToList();


					List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();

					foreach (var item in listadoEstructura)
					{
						EstructuraCapituloProgramaBO objeto = new EstructuraCapituloProgramaBO();
						objeto.OrdenFila = item.Key;
						foreach (var itemRegistros in item)
						{
							switch (itemRegistros.NombreTitulo)
							{
								case "Capitulo":
									objeto.Nombre = itemRegistros.Nombre;
									objeto.Capitulo = itemRegistros.Contenido;
									objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
									objeto.IdPgeneral = itemRegistros.IdPgeneral;
									break;
								case "Sesion":
									objeto.Sesion = itemRegistros.Contenido;
									objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
									objeto.IdDocumentoSeccionPw = itemRegistros.Id;
									break;
								case "SubSeccion":
									objeto.SubSesion = itemRegistros.Contenido;
									objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
									if (!string.IsNullOrEmpty(objeto.SubSesion))
									{
										objeto.IdDocumentoSeccionPw = itemRegistros.Id;
									}
									break;
								default:
									objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
									objeto.TotalSegundos = itemRegistros.TotalSegundos;
									break;
							}
						}
						lista.Add(objeto);
					}

					var rpta = lista.OrderBy(x => x.OrdenFila).ToList();

					var listas = rpta.GroupBy(x => new { x.IdPgeneral, x.Nombre, x.Capitulo, x.OrdenCapitulo });

					listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();

					foreach (var capitulo in listas)
					{
						CapituloSesionProgramaCapacitacionDTO registro = new CapituloSesionProgramaCapacitacionDTO();
						registro.IdPGeneral = capitulo.Key.IdPgeneral;
						registro.CapituloProgramaCapacitacion = capitulo.Key.Capitulo;
						registro.IdCapituloProgramaCapacitacion = capitulo.Key.OrdenCapitulo;

						registro.ListaSesionesProgramaCapacitacion = new List<SesionSubSeccionProgramaCapacitacionDTO>();

						registro.ListaSesionesProgramaCapacitacion = capitulo.GroupBy(x => x.Sesion).Select(x => new SesionSubSeccionProgramaCapacitacionDTO {
							SesionProgramaCapacitacion = x.Key,
							IdSesionProgramaCapacitacion = capitulo.GroupBy(z => new { z.OrdenFila, z.Sesion, z.SubSesion }).Where(z => z.Key.Sesion == x.Key).FirstOrDefault().Key.OrdenFila,
							ListaSubSeccionProgramaCapacitacion = capitulo.GroupBy(y => new { y.OrdenFila, y.Sesion, y.SubSesion }).Where(y => y.Key.Sesion == x.Key).Select(y => new SubSeccionProgramaCapacitacionDTO { 
								IdSesionProgramaCapacitacion = y.Key.OrdenFila,
								SubSeccionProgramaCapacitacion = y.Key.SubSesion
							}).ToList()
						}).ToList();

						listaRegistro.Add(registro);
					}
				}
				return listaRegistro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
