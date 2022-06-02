using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using System.Transactions;
using Google.Api.Ads.Common.Util;
using CsvHelper;
using System.IO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaestroPregunta")]
    public class MaestroPreguntaController : ControllerBase
    {

		private readonly integraDBContext _integraDBContext;
		private readonly PreguntaRepositorio _repPregunta;
		private readonly PreguntaTipoRepositorio _repPreguntaTipo;
		private readonly RespuestaPreguntaRepositorio _repRespuestaPregunta;
		private readonly ExamenRepositorio _repExamen;
		private readonly PreguntaIntentoRepositorio _repPreguntaIntento;
		private readonly AsignacionPreguntaExamenRepositorio _repAsignacionPreguntaExamen;
		private readonly PreguntaCategoriaRepositorio _repPreguntaCategoria;

		public MaestroPreguntaController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
			_repPregunta = new PreguntaRepositorio(_integraDBContext);
			_repPreguntaTipo = new PreguntaTipoRepositorio(_integraDBContext);
			_repRespuestaPregunta = new RespuestaPreguntaRepositorio(_integraDBContext);
			_repExamen = new ExamenRepositorio(_integraDBContext);
			_repPreguntaIntento = new PreguntaIntentoRepositorio(_integraDBContext);
			_repAsignacionPreguntaExamen = new AsignacionPreguntaExamenRepositorio(_integraDBContext);
			_repPreguntaCategoria = new PreguntaCategoriaRepositorio(_integraDBContext);
		}

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
				var examen = _repExamen.ObtenerTodoFiltro();
				var tipoRespuestaCalificacion = _repRespuestaPregunta.ObtenerTipoRespuestaCalificacion();
				var preguntaCategoria = _repPreguntaCategoria.ObtenerCategoriasPreguntasRegistradas();
				return Ok(new { PreguntaTipo = preguntaTipo, Examen = examen, TipoRespuestaCalificacion = tipoRespuestaCalificacion, PreguntaCategoria = preguntaCategoria });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

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
				var preguntas = _repPregunta.ObtenerPreguntasRegistradas();
				var agrupado = preguntas.GroupBy(x => new
				{
					x.Id,
					x.Enunciado,
					x.IdTipoRespuesta,
					x.IdPreguntaTipo,
					x.MinutosPorPregunta,
					x.RespuestaAleatoria,
					x.ActivarFeedBackRespuestaCorrecta,
					x.ActivarFeedBackRespuestaIncorrecta,
					x.MostrarFeedbackInmediato,
					x.MostrarFeedbackPorPregunta,
					x.NumeroMaximoIntento,
					x.ActivarFeedbackMaximoIntento,
					x.MensajeFeedbackIntento,
					x.IdTipoRespuestaCalificacion,
					x.FactorRespuesta,
					x.IdPreguntaCategoria
				}).Select(x => new PreguntaAgrupadaDTO
				{
					Id = x.Key.Id,
					Enunciado = x.Key.Enunciado,
					IdTipoRespuesta = x.Key.IdTipoRespuesta,
					IdPreguntaTipo = x.Key.IdPreguntaTipo,
					MinutosPorPregunta = x.Key.MinutosPorPregunta,
					RespuestaAleatoria = x.Key.RespuestaAleatoria,
					ActivarFeedBackRespuestaCorrecta = x.Key.ActivarFeedBackRespuestaCorrecta,
					ActivarFeedBackRespuestaIncorrecta = x.Key.ActivarFeedBackRespuestaIncorrecta,
					MostrarFeedbackInmediato = x.Key.MostrarFeedbackInmediato,
					MostrarFeedbackPorPregunta = x.Key.MostrarFeedbackPorPregunta,
					NumeroMaximoIntento = x.Key.NumeroMaximoIntento,
					ActivarFeedbackMaximoIntento = x.Key.ActivarFeedbackMaximoIntento,
					MensajeFeedbackIntento = x.Key.MensajeFeedbackIntento,
					IdTipoRespuestaCalificacion = x.Key.IdTipoRespuestaCalificacion,
					FactorRespuesta = x.Key.FactorRespuesta,
					IdPreguntaCategoria = x.Key.IdPreguntaCategoria,
					ComponenteExamen = x.GroupBy(y => y.ComponenteExamen).Select(y => y.Key).ToList(),
					ListaExamen = x.GroupBy(z => z.IdExamen).Select(z => z.Key).ToList()
				}).ToList();
				return Ok(agrupado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
        }

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerRespuestasPregunta([FromBody]int IdPregunta)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<RespuestaPreguntaDTO> res;
				if (IdPregunta > 0)
				{
					res = _repRespuestaPregunta.ObtenerRespuestaPregunta(IdPregunta);
					return Ok(res);
				}
				else
				{
					res = new List<RespuestaPreguntaDTO>();
					return Ok(res);
				}
				
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarPregunta([FromBody]CompuestoPreguntaDTO Filtro)
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

					PreguntaBO pregunta = new PreguntaBO()
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
						IdPreguntaCategoria = Filtro.Pregunta.IdPreguntaCategoria
					};
					_repPregunta.Insert(pregunta);

					foreach (var item in Filtro.Examen.ListaExamen)
					{
						AsignacionPreguntaExamenBO asignacionPreguntaExamen = new AsignacionPreguntaExamenBO()
						{
							IdExamen = item,
							IdPregunta = pregunta.Id,
							NroOrden = 1,
							Puntaje = 1,
							Estado = true,
							UsuarioCreacion = Filtro.Usuario,
							UsuarioModificacion = Filtro.Usuario,
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
						};
						_repAsignacionPreguntaExamen.Insert(asignacionPreguntaExamen);
					}
					
					foreach (var item in Filtro.RespuestaPregunta)
					{
						int? puntajeTipoRespuesta = null;
						if (Filtro.Pregunta.IdTipoRespuestaCalificacion.HasValue)
						{
							int tipoRes = Filtro.Pregunta.IdTipoRespuestaCalificacion.Value;
							if (tipoRes == 1) //Directo
							{
								puntajeTipoRespuesta = item.Puntaje ;
							}
							else if(tipoRes == 2) //Inversa
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
						RespuestaPreguntaBO respuestaPregunta = new RespuestaPreguntaBO()
						{
							IdPregunta = pregunta.Id,
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
						_repRespuestaPregunta.Insert(respuestaPregunta);
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

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarPregunta([FromBody]CompuestoPreguntaDTO Filtro)
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
					List<AsignacionPreguntaExamenBO> listaAsignacionPreguntaExamen;
					var pregunta = _repPregunta.FirstById(Filtro.Pregunta.Id);
					if (pregunta.IdPreguntaIntento.HasValue)
					{
						preguntaIntento = _repPreguntaIntento.FirstById(pregunta.IdPreguntaIntento.Value);
						preguntaIntento.NumeroMaximoIntento = Filtro.PreguntaIntento.NumeroMaximoIntento;
						preguntaIntento.ActivarFeedbackMaximoIntento = Filtro.PreguntaIntento.ActivarFeedbackMaximoIntento;
						preguntaIntento.MensajeFeedback = Filtro.PreguntaIntento.MensajeFeedbackIntento;
						preguntaIntento.UsuarioModificacion = Filtro.Usuario;
						preguntaIntento.FechaModificacion = DateTime.Now;
						_repPreguntaIntento.Update(preguntaIntento);
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
					pregunta.IdPreguntaCategoria = Filtro.Pregunta.IdPreguntaCategoria;
					_repPregunta.Update(pregunta);

					listaAsignacionPreguntaExamen = _repAsignacionPreguntaExamen.GetBy(x => x.IdPregunta == pregunta.Id).ToList();
					foreach (var item in listaAsignacionPreguntaExamen)
					{
						if(!Filtro.Examen.ListaExamen.Any(x => x == item.IdExamen))
						{
							_repAsignacionPreguntaExamen.Delete(item.Id, Filtro.Usuario);
						}
						else
						{

						}
					}
					listaAsignacionPreguntaExamen = _repAsignacionPreguntaExamen.GetBy(x => x.IdPregunta == pregunta.Id).ToList();
					foreach (var item in Filtro.Examen.ListaExamen)
					{
						AsignacionPreguntaExamenBO asignacionPreguntaExamen;
						if (listaAsignacionPreguntaExamen.Any(x => x.IdExamen == item))
						{
							asignacionPreguntaExamen = _repAsignacionPreguntaExamen.FirstBy(x => x.IdExamen == item);
							asignacionPreguntaExamen.NroOrden = 1;
							asignacionPreguntaExamen.Puntaje = 1;
							asignacionPreguntaExamen.UsuarioModificacion = Filtro.Usuario;
							asignacionPreguntaExamen.FechaModificacion = DateTime.Now;

							_repAsignacionPreguntaExamen.Update(asignacionPreguntaExamen);
						}
						else
						{
							asignacionPreguntaExamen = new AsignacionPreguntaExamenBO();
							asignacionPreguntaExamen.IdExamen = item;
							asignacionPreguntaExamen.IdPregunta = pregunta.Id;
							asignacionPreguntaExamen.NroOrden = 1;
							asignacionPreguntaExamen.Puntaje = 1;
							asignacionPreguntaExamen.Estado = true;
							asignacionPreguntaExamen.UsuarioCreacion = Filtro.Usuario;
							asignacionPreguntaExamen.UsuarioModificacion = Filtro.Usuario;
							asignacionPreguntaExamen.FechaCreacion = DateTime.Now;
							asignacionPreguntaExamen.FechaModificacion = DateTime.Now;

							_repAsignacionPreguntaExamen.Insert(asignacionPreguntaExamen);
						}
					}
					var rp = _repRespuestaPregunta.GetBy(x => x.IdPregunta == pregunta.Id);
					foreach(var item in rp)
					{
						if(!Filtro.RespuestaPregunta.Any(x => x.Id == item.Id))
						{
							_repRespuestaPregunta.Delete(item.Id, Filtro.Usuario);
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

						RespuestaPreguntaBO respuestaPregunta;
						if (item.Id > 0)
						{
							respuestaPregunta = _repRespuestaPregunta.FirstById(item.Id);
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
							_repRespuestaPregunta.Update(respuestaPregunta);
						}
						else
						{
							respuestaPregunta = new RespuestaPreguntaBO()
							{
								IdPregunta = pregunta.Id,
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
							_repRespuestaPregunta.Insert(respuestaPregunta);
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
					var pregunta = _repPregunta.FirstById(Filtro.Id);
					if(pregunta != null)
					{
						var listaAsignacionPreguntaExamen = _repAsignacionPreguntaExamen.GetBy(x => x.IdPregunta == pregunta.Id);
						var respuestaPregunta = _repRespuestaPregunta.GetBy(x => x.IdPregunta == pregunta.Id);

						if (pregunta.IdPreguntaIntento.HasValue)
						{
							_repPreguntaIntento.Delete(pregunta.IdPreguntaIntento.Value, Filtro.NombreUsuario);
						}

						foreach (var item in listaAsignacionPreguntaExamen)
						{
							_repAsignacionPreguntaExamen.Delete(item.Id, Filtro.NombreUsuario);
						}
						foreach(var item in respuestaPregunta)
						{
							_repRespuestaPregunta.Delete(item.Id, Filtro.NombreUsuario);
						}
						_repPregunta.Delete(pregunta.Id, Filtro.NombreUsuario);
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



		[Route("[action]")]
		[HttpPost]
		public ActionResult ImportarExcel([FromForm] IFormFile files)
		{
			CsvFile file = new CsvFile();
			List<string> listaErrores = new List<string>();
			try
			{
				int indexError = 0;
				int indexTotal = 0;

				using (var cvs = new CsvReader(new StreamReader(files.OpenReadStream())))
				{
					cvs.Configuration.Delimiter = ";";
					cvs.Configuration.MissingFieldFound = null;
					cvs.Read();
					cvs.ReadHeader();
					while (cvs.Read())
					{
						indexTotal++;
						try
						{
							using (TransactionScope scope = new TransactionScope())
							{
								PreguntaIntentoBO preguntaIntento = new PreguntaIntentoBO()
								{
									NumeroMaximoIntento = cvs.GetField<int?>("NumeroMaximoIntento"),
									ActivarFeedbackMaximoIntento = cvs.GetField<bool?>("ActivarFeedbackMaximoIntento"),
									MensajeFeedback = cvs.GetField<string>("MensajeFeedback"),
									Estado = true,
									UsuarioCreacion = "ImportarExcel",
									UsuarioModificacion = "ImportarExcel",
									FechaCreacion = DateTime.Now,
									FechaModificacion = DateTime.Now
								};
								_repPreguntaIntento.Insert(preguntaIntento);

								PreguntaBO pregunta = new PreguntaBO()
								{
									IdTipoRespuesta = cvs.GetField<int?>("IdTipoRespuesta"),
									EnunciadoPregunta = cvs.GetField<string>("EnunciadoPregunta"),
									MinutosPorPregunta = cvs.GetField<int?>("MinutosPorPregunta"),
									RespuestaAleatoria = cvs.GetField<bool?>("RespuestaAleatoria"),
									ActivarFeedBackRespuestaCorrecta = cvs.GetField<bool?>("ActivarFeedBackRespuestaCorrecta"),
									ActivarFeedBackRespuestaIncorrecta = cvs.GetField<bool?>("ActivarFeedBackRespuestaIncorrecta"),
									MostrarFeedbackInmediato = cvs.GetField<bool?>("MostrarFeedbackInmediato"),
									MostrarFeedbackPorPregunta = cvs.GetField<bool?>("MostrarFeedbackPorPregunta"),
									Estado = true,
									UsuarioCreacion = "ImportarExcel",
									UsuarioModificacion = "ImportarExcel",
									FechaCreacion = DateTime.Now,
									FechaModificacion = DateTime.Now,
									IdPreguntaIntento = preguntaIntento.Id,
									IdPreguntaTipo = cvs.GetField<int?>("IdPreguntaTipo"),
									IdTipoRespuestaCalificacion = cvs.GetField<int?>("IdTipoRespuestaCalificacion"),
									FactorRespuesta = cvs.GetField<int?>("FactorRespuesta"),
									IdPreguntaCategoria = cvs.GetField<int?>("IdPreguntaCategoria")
								};
								_repPregunta.Insert(pregunta);

								AsignacionPreguntaExamenBO asignacionPreguntaExamen = new AsignacionPreguntaExamenBO()
								{
									IdExamen = cvs.GetField<int>("IdExamen"),
									IdPregunta = pregunta.Id,
									NroOrden = 1,
									Puntaje = 1,
									Estado = true,
									UsuarioCreacion = "ImportarExcel",
									UsuarioModificacion = "ImportarExcel",
									FechaCreacion = DateTime.Now,
									FechaModificacion = DateTime.Now
								};
								_repAsignacionPreguntaExamen.Insert(asignacionPreguntaExamen);
								scope.Complete();
							}
						}
						catch (Exception e)
						{
							indexError++;
							listaErrores.Add("Error en: " + cvs.GetField<string>("EnunciadoPregunta") + " - " + e.Message);
						}
						
					}
				}
				return Ok(new { Total = indexTotal,  Correctos = (indexTotal - indexError) , Error = indexError, Errores = listaErrores });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ImportarRespuestasExcel([FromForm] ImportarRespuestasExcelDTO Dto)
		{
			CsvFile file = new CsvFile();
			List<string> listaErrores = new List<string>();
			try
			{
				int indexError = 0;
				int indexTotal = 0;
				var pregunta = _repPregunta.FirstById(Dto.IdPregunta);

				using (var cvs = new CsvReader(new StreamReader(Dto.File.OpenReadStream())))
				{
					cvs.Configuration.Delimiter = ";";
					cvs.Configuration.MissingFieldFound = null;
					cvs.Read();
					cvs.ReadHeader();
					while (cvs.Read())
					{
						int? puntajeTipoRespuesta = null;
						int? puntaje = cvs.GetField<int?>("Puntaje");
						bool? respuestaCorrecta = cvs.GetField<bool?>("RespuestaCorrecta");
						if (pregunta.IdTipoRespuestaCalificacion.HasValue)
						{
							int tipoRes = pregunta.IdTipoRespuestaCalificacion.Value;
							if (tipoRes == 1) //Directo
							{
								puntajeTipoRespuesta = puntaje;
							}
							else if (tipoRes == 2) //Inversa
							{
								if (pregunta.FactorRespuesta.HasValue)
								{
									int factorRes = pregunta.FactorRespuesta.Value;
									puntajeTipoRespuesta = factorRes - puntaje;
								}
							}
							else //negativo
							{
								if (pregunta.FactorRespuesta.HasValue)
								{
									int factorRes = pregunta.FactorRespuesta.Value;
									if (respuestaCorrecta.HasValue)
										if (!respuestaCorrecta.Value)
											puntajeTipoRespuesta = puntaje - factorRes;
										else
											puntajeTipoRespuesta = puntaje;
								}
							}
						}

						indexTotal++;
						try
						{
							using (TransactionScope scope = new TransactionScope())
							{
								RespuestaPreguntaBO respuestaPregunta = new RespuestaPreguntaBO()
								{
									IdPregunta = Dto.IdPregunta,
									RespuestaCorrecta = cvs.GetField<bool?>("RespuestaCorrecta"),
									NroOrden = cvs.GetField<int>("NroOrden"),
									EnunciadoRespuesta = cvs.GetField<string>("EnunciadoRespuesta"),
									NroOrdenRespuesta = cvs.GetField<int?>("NroOrdenRespuesta"),
									Puntaje = cvs.GetField<int?>("Puntaje"),
									FeedbackPositivo = cvs.GetField<string>("FeedbackPositivo"),
									FeedbackNegativo = cvs.GetField<string>("FeedbackNegativo"),
									Estado = true,
									UsuarioCreacion = Dto.Usuario,
									UsuarioModificacion = Dto.Usuario,
									FechaCreacion = DateTime.Now,
									FechaModificacion = DateTime.Now,
									PuntajeTipoRespuesta = puntajeTipoRespuesta
								};
								_repRespuestaPregunta.Insert(respuestaPregunta);
								scope.Complete();
							}
						}
						catch (Exception e)
						{
							indexError++;
							listaErrores.Add("Error en: " + cvs.GetField<string>("EnunciadoPregunta") + " - " + e.Message);
						}

					}
				}
				return Ok(new { Total = indexTotal, Correctos = (indexTotal - indexError), Error = indexError, Errores = listaErrores });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}
	}
}
