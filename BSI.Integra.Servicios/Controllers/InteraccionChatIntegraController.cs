using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/InteraccionChatIntegra")]
	public class InteraccionChatIntegraController : BaseController<TInteraccionChatIntegra, ValidadorInteraccionChatIntegrajsonDTO>
	{
		public InteraccionChatIntegraController(IIntegraRepository<TInteraccionChatIntegra> repositorio, ILogger<BaseController<TInteraccionChatIntegra, ValidadorInteraccionChatIntegrajsonDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
		{
		}

		[Route("[action]/{connectionId}")]
		[HttpGet]
		public ActionResult ObtenerInteraccionChatIntegraPorConnectionId(string connectionId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				//InteraccionChatIntegraBO interaccionChatIntegra = new InteraccionChatIntegraBO();
				InteraccionChatIntegraRepositorio _repInteraccionChatIntegra = new InteraccionChatIntegraRepositorio();
				return Ok(_repInteraccionChatIntegra.ObtenerInteraccionChatIntegraPorConnectionId(connectionId));
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[Route("[action]/{listIdsChatSession}")]
		[HttpGet]
		public ActionResult ObtenerInteraccionesPorIdsSessionChat(string listIdsChatSession)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatIntegraRepositorio interaccionChatIntegra = new InteraccionChatIntegraRepositorio();
				return Ok(interaccionChatIntegra.ObtenerInteraccionesPorIdsSessionChat(listIdsChatSession));
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult Insertar([FromBody] InteraccionChatIntegraDTO jsonDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatIntegraRepositorio _repInteraccionChatIntegra = new InteraccionChatIntegraRepositorio();
				int idConjuntoAnuncio;
				if (!string.IsNullOrEmpty(jsonDTO.IdConjuntoAnuncio) && !jsonDTO.IdConjuntoAnuncio.Equals("0"))
				{
					ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
					if (jsonDTO.IdConjuntoAnuncio.Length < 32)
					{
						try
						{
							int tmp = Convert.ToInt32(jsonDTO.IdConjuntoAnuncio);
							var res = _repConjuntoAnuncio.FirstById(tmp);
							if(res != null)
							{
								idConjuntoAnuncio = res.Id;
							}
							else
							{
								idConjuntoAnuncio = 1;
							}
						}
						catch (Exception e)
						{
							idConjuntoAnuncio = 1;
						}
					}
					else
					{
						var res = _repConjuntoAnuncio.FirstBy(x => x.IdMigracion == Guid.Parse(jsonDTO.IdConjuntoAnuncio));
						if(res != null)
						{
							idConjuntoAnuncio = res.Id;
						}
						else
						{
							idConjuntoAnuncio = 1;
						}
					}
				}
				else
				{
					idConjuntoAnuncio = 1;
				}
				InteraccionChatIntegraBO interaccionChatIntegraBO = new InteraccionChatIntegraBO
				{
					IdChatIntegraHistorialAsesor = jsonDTO.IdChatIntegraHistorialAsesor,
					IdAlumno = jsonDTO.IdAlumno,
					IdContactoPortalSegmento = jsonDTO.IdContactoPortalSegmento,
					IdTipoInteraccion = jsonDTO.IdTipoInteraccion,
					IdPgeneral = jsonDTO.IdPgeneral,
					IdSubAreaCapacitacion = jsonDTO.IdSubAreaCapacitacion,
					IdAreaCapacitacion = jsonDTO.IdAreaCapacitacion,
					Ip = jsonDTO.Ip,
					Pais = jsonDTO.Pais,
					Region = jsonDTO.Region,
					Ciudad = jsonDTO.Ciudad,
					Duracion = jsonDTO.Duracion,
					NroMensajes = jsonDTO.NroMensajes,
					NroPalabrasVisitor = jsonDTO.NroPalabrasVisitor,
					NroPalabrasAgente = jsonDTO.NroPalabrasAgente,
					UsuarioTiempoRespuestaMaximo = jsonDTO.UsuarioTiempoRespuestaMaximo,
					UsuarioTiempoRespuestaPromedio = jsonDTO.UsuarioTiempoRespuestaPromedio,
					FechaInicio = DateTime.Now,
					FechaFin = DateTime.Now,
					Leido = jsonDTO.Leido,
					Plataforma = jsonDTO.Plataforma,
					Navegador = jsonDTO.Navegador,
					UrlFrom = jsonDTO.UrlFrom,
					UrlTo = jsonDTO.UrlTo,
					IdEstadoChat = jsonDTO.IdEstadoChat,
					IdConjuntoAnuncio = idConjuntoAnuncio,
					IdChatSession = jsonDTO.IdChatSession,
					IdFaseOportunidadPortalWeb = jsonDTO.IdFaseOportunidadPortalWeb,
					Estado = true,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
					UsuarioCreacion = jsonDTO.UsuarioCreacion,
					UsuarioModificacion = jsonDTO.UsuarioModificacion,
					ContadorUsuarioPromedioRespuesta = jsonDTO.ContadorUsuarioPromedioRespuesta,
					TiempoRespuestaTotal = jsonDTO.TiempoRespuestaTotal
				};

				_repInteraccionChatIntegra.Insert(interaccionChatIntegraBO);
				return Ok(interaccionChatIntegraBO.Id);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarInteraccionChatIntegra([FromBody] InteraccionChatIntegraDTO jsonDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatIntegraRepositorio _repInteraccionChatIntegra = new InteraccionChatIntegraRepositorio();

				if (_repInteraccionChatIntegra.Exist(jsonDTO.Id))
				{
					var interaccionChatIntegraBO = _repInteraccionChatIntegra.FirstById(jsonDTO.Id);
					interaccionChatIntegraBO.FechaModificacion = DateTime.Now;
					interaccionChatIntegraBO.UsuarioModificacion = "Signal";
					if (jsonDTO.Tipo == 1) //asesor
					{
						interaccionChatIntegraBO.FechaFin = DateTime.Now;
						interaccionChatIntegraBO.Duracion = Convert.ToInt32(interaccionChatIntegraBO.FechaFin.Value.Subtract(interaccionChatIntegraBO.FechaInicio.Value).TotalSeconds);
						interaccionChatIntegraBO.NroMensajes = (interaccionChatIntegraBO.NroMensajes == null ? 0 : interaccionChatIntegraBO.NroMensajes.Value) + 1;
						interaccionChatIntegraBO.NroPalabrasAgente = (interaccionChatIntegraBO.NroPalabrasAgente == null ? 0 : interaccionChatIntegraBO.NroPalabrasAgente.Value) + 1;
					}
					if (jsonDTO.Tipo == 2) //visitante
					{
						interaccionChatIntegraBO.FechaFin = DateTime.Now;
						interaccionChatIntegraBO.Duracion = Convert.ToInt32(interaccionChatIntegraBO.FechaFin.Value.Subtract(interaccionChatIntegraBO.FechaInicio.Value).TotalSeconds);
						interaccionChatIntegraBO.NroMensajes = (interaccionChatIntegraBO.NroMensajes == null ? 0 : interaccionChatIntegraBO.NroMensajes.Value) + 1;
						interaccionChatIntegraBO.NroPalabrasVisitor = (interaccionChatIntegraBO.NroPalabrasVisitor == null ? 0 : interaccionChatIntegraBO.NroPalabrasVisitor.Value) + 1;
						interaccionChatIntegraBO.Leido = jsonDTO.Leido;
						interaccionChatIntegraBO.NroMensajesSinLeer = (interaccionChatIntegraBO.NroMensajesSinLeer == null ? 0 : interaccionChatIntegraBO.NroMensajesSinLeer.Value) + 1;
					}
					if (jsonDTO.Tipo == 3) //otro
					{
						interaccionChatIntegraBO.IdAlumno = jsonDTO.IdAlumno;
						interaccionChatIntegraBO.IdFaseOportunidadPortalWeb = jsonDTO.IdFaseOportunidadPortalWeb;
					}
					if (jsonDTO.Tipo == 4) //otro
					{
						interaccionChatIntegraBO.FechaFin = DateTime.Now;
						interaccionChatIntegraBO.Leido = jsonDTO.Leido;
						interaccionChatIntegraBO.NroMensajesSinLeer = 0;

					}
					_repInteraccionChatIntegra.Update(interaccionChatIntegraBO);
					return Ok(interaccionChatIntegraBO);
				}
				else
				{
					return Ok();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarInteraccionChatIntegraEsperaCliente([FromBody] InteraccionChatIntegraDTO jsonDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatIntegraRepositorio _repInteraccionChatIntegra = new InteraccionChatIntegraRepositorio();
				ChatDetalleIntegraRepositorio _repChatDetalleIntegra = new ChatDetalleIntegraRepositorio();

				if (_repInteraccionChatIntegra.Exist(jsonDTO.Id))
				{

					var interaccionChatIntegraBO = _repInteraccionChatIntegra.FirstById(jsonDTO.Id);

					var chatDetalleIntegra = _repChatDetalleIntegra.GetBy(x => x.IdInteraccionChatIntegra == interaccionChatIntegraBO.Id && x.IdRemitente.Equals("asesor")).ToList();

					if(chatDetalleIntegra.Count == 0)
					{
						TimeSpan tiempoEspera = DateTime.Now.Subtract(interaccionChatIntegraBO.FechaInicio == null? DateTime.Now : interaccionChatIntegraBO.FechaInicio.Value);
						interaccionChatIntegraBO.ClienteTiempoEspera = Convert.ToDecimal(tiempoEspera.TotalSeconds);

						interaccionChatIntegraBO.FechaModificacion = DateTime.Now;
						interaccionChatIntegraBO.UsuarioModificacion = "Signal";
						interaccionChatIntegraBO.Estado = true;
						_repInteraccionChatIntegra.Update(interaccionChatIntegraBO);
					}

					//interaccionChatIntegraBO.Id = jsonDTO.Id;

					
					return Ok(interaccionChatIntegraBO);
				}
				else
				{
					return Ok();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[Route("[action]/{IdInteraccionChat}")]
		[HttpGet]
		public ActionResult ActualizarIdAlumno(int IdInteraccionChat)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				int? idalumno = null;
				FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
				InteraccionChatIntegraRepositorio _repInteraccionChatIntegra = new InteraccionChatIntegraRepositorio();
				ChatDetalleIntegraRepositorio _repChatDetalleIntegra = new ChatDetalleIntegraRepositorio();
				OportunidadDatosChatDTO oportunidadValores = null;
				var idfaseoportunidadPortal = _repFaseOportunidad.ObtenerFaseOportunidadPorInteraccionId(IdInteraccionChat).IdFaseOportunidadPortal;
				var Alumno = _repChatDetalleIntegra.FirstBy(x => x.IdInteraccionChatIntegra == IdInteraccionChat && x.IdRemitente.Equals("visitante"));
				var interaccionChatIntegra = _repInteraccionChatIntegra.FirstById(IdInteraccionChat);
				if (idfaseoportunidadPortal != "00000000-0000-0000-0000-000000000000")
				{
					oportunidadValores = _repFaseOportunidad.ObtenerOportunidadDatosChatPorIdFaseOportunidadPortal(idfaseoportunidadPortal);
					if (oportunidadValores == null)
					{
						oportunidadValores = _repFaseOportunidad.ObtenerOportunidadDatosChatPorIdFaseOportunidadPortalAA(idfaseoportunidadPortal);
					}
					
					interaccionChatIntegra.IdAlumno = oportunidadValores.IdContacto;
					interaccionChatIntegra.UsuarioModificacion = "SYSTEM";
					interaccionChatIntegra.FechaModificacion = DateTime.Now;
					_repInteraccionChatIntegra.Update(interaccionChatIntegra);
					return Ok(new { NombreAlumno = string.Concat(oportunidadValores.Nombre1, " ", oportunidadValores.ApellidoPaterno, (string.IsNullOrEmpty(oportunidadValores.ApellidoMaterno) == true ? "" : " " +oportunidadValores.ApellidoMaterno)), IdAlumno = oportunidadValores.IdContacto });
				}
				else
				{
					var nombreAlumno = "";
					interaccionChatIntegra.IdAlumno = idalumno;
					interaccionChatIntegra.UsuarioModificacion = "SYSTEM";
					interaccionChatIntegra.FechaModificacion = DateTime.Now;
					_repInteraccionChatIntegra.Update(interaccionChatIntegra);
					if (Alumno != null)
					{
						nombreAlumno = Alumno.IdRemitente;
					}
					return Ok(new { NombreAlumno = nombreAlumno, IdAlumno = idalumno });
				}
			
				
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ReporteChat([FromBody] ChatReporteDTO data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatIntegraRepositorio _repInteraccionChatIntegra = new InteraccionChatIntegraRepositorio();
				var chat = _repInteraccionChatIntegra.GenerarReporteChat(data).OrderByDescending(x => x.Fecha).ToList();
				//var sinChat = _repInteraccionChatIntegra.GenerarReporteChatSinChat(data);

				//var agrupado = (from p in chat
				//				group p by new { p.Fecha, p.Pais } into grupo
				//				select new { g = grupo.Key, l = grupo }).ToList();

				IEnumerable<ChatAgrupadoDTO> agrupado = null;
				if (data.Desglose == 1) {
					agrupado = chat.GroupBy(x => x.Pais)
					.Select(g => new ChatAgrupadoDTO
					{
						Pais = g.Key,
						Detalle = g.GroupBy(x => x.Fecha).Select(x => new ChatIntegraDetalleDTO
						{
							Fecha = x.Key.ToString("yyyyMMdd"),
							Detalle = x.Select(y => new ChatIntegraSubDetalleDTO
							{
								Area = y.Area,
								Asesor = y.Asesor,
								Chats = y.Chats,
								ClickEmpezar = y.ClickEmpezar,
								Logueados = y.Logueados,
								PalabrasVisitante = y.PalabrasVisitante,
								Oportunidades = y.Oportunidades,
								Promedio = y.Promedio,
								Atendidos = y.Atendidos,
								ClienteTiempoEspera = y.ClienteTiempoEspera
								//NoAtendidos = y.NoAtendidos

							}).ToList()
						}).ToList()
					});
				}
				
				if(data.Desglose == 2)
				{
					agrupado = chat.GroupBy(x => x.Pais)
					.Select(g => new ChatAgrupadoDTO
					{
						Pais = g.Key,
						Detalle = g.GroupBy(x => _repInteraccionChatIntegra.ObtenerNumeroSemana(x.Fecha)).Select(x => new ChatIntegraDetalleDTO
						{
							Fecha = "Semana_" + x.Key,
							Detalle = x.Select(y => new ChatIntegraSubDetalleDTO
							{
								Area = y.Area,
								Asesor = y.Asesor,
								Chats = y.Chats,
								ClickEmpezar = y.ClickEmpezar,
								Logueados = y.Logueados,
								PalabrasVisitante = y.PalabrasVisitante,
								Oportunidades = y.Oportunidades,
								Promedio = y.Promedio,
								Atendidos = y.Atendidos,
								ClienteTiempoEspera = y.ClienteTiempoEspera
								//NoAtendidos = y.NoAtendidos

							}).ToList()
						}).ToList()
					});
				}

				if (data.Desglose == 3)
				{
					agrupado = chat.GroupBy(x => x.Pais)
					.Select(g => new ChatAgrupadoDTO
					{
						Pais = g.Key,
						Detalle = g.GroupBy(x => x.Fecha.Month).Select(x => new ChatIntegraDetalleDTO
						{
							Fecha = _repInteraccionChatIntegra.ObtenerNombreMes(x.Key),
							Detalle = x.Select(y => new ChatIntegraSubDetalleDTO
							{
								Area = y.Area,
								Asesor = y.Asesor,
								Chats = y.Chats,
								ClickEmpezar = y.ClickEmpezar,
								Logueados = y.Logueados,
								PalabrasVisitante = y.PalabrasVisitante,
								Oportunidades = y.Oportunidades,
								Promedio = y.Promedio,
								Atendidos = y.Atendidos,
								ClienteTiempoEspera = y.ClienteTiempoEspera
								//NoAtendidos = y.NoAtendidos

							}).ToList()
						}).ToList()
					});
				}


				//var agrupadosinChat = (from p in sinChat
				//					   group p by p.Fecha into grupo
				//					   select new { g = grupo.Key, l = grupo }).ToList();

				return Ok(new { Chat = agrupado });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombosReporteChat()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
				AreaCapacitacionRepositorio _repAreaCapacitacion = new AreaCapacitacionRepositorio();
				PersonalRepositorio _repPersonal = new PersonalRepositorio();
				PaisRepositorio _repPais = new PaisRepositorio();

				var centroCosto = _repCentroCosto.ObtenerCentroCostoParaFiltro();
				var areaCapacitacion = _repAreaCapacitacion.ObtenerTodoFiltro();
				var personal = _repPersonal.CargarPersonalParaFiltro();
				var pais = _repPais.ObtenerPaisesCombo();

				return Ok(new { CentroCosto = centroCosto, AreaCapacitacion = areaCapacitacion, Personal = personal, Pais = pais });

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ReporteChatLog([FromBody] ChatReporteDTO data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatIntegraRepositorio _repInteraccionChatIntegra = new InteraccionChatIntegraRepositorio();
				var chatLog = _repInteraccionChatIntegra.GenerarReporteChatLog(data);

				return Ok(chatLog);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]/{Fecha}")]
		[HttpGet]
		public ActionResult RegularizarInteracciones(DateTime Fecha)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatIntegraRepositorio _repInteraccionChatIntegra = new InteraccionChatIntegraRepositorio();
				var interaccion = _repInteraccionChatIntegra.GetBy(x => x.FechaCreacion.Date >= Fecha.Date && (x.IdAlumno == 0 || x.IdAlumno == null)).ToList();
				var total = interaccion.Count;
				foreach(var item in interaccion)
				{
					this.ActualizarIdAlumno(item.Id);
				}
				interaccion = _repInteraccionChatIntegra.GetBy(x => x.FechaCreacion.Date >= Fecha.Date && (x.IdAlumno == 0 || x.IdAlumno == null)).ToList();
				var resto = interaccion.Count;
				string res = "Total: " + total + "\nProcesado: " + (total - resto) + "\nResta: " + resto;
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}

	//Validador del objeto
	public class ValidadorInteraccionChatIntegrajsonDTO : AbstractValidator<TInteraccionChatIntegra>
	{
		public static ValidadorInteraccionChatIntegrajsonDTO Current = new ValidadorInteraccionChatIntegrajsonDTO();
		public ValidadorInteraccionChatIntegrajsonDTO()
		{
			RuleFor(objeto => objeto.Id).NotNull().WithMessage("Id es Obligatorio, no puede ser nulo");
		}
	}
}