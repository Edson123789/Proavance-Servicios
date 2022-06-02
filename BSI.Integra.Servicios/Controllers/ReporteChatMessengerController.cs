using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/ReporteChatMessenger")]
	[ApiController]
	public class ReporteChaMessengerController : Controller
	{
		private readonly integraDBContext _integraDBContext;
		public ReporteChaMessengerController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
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
				PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
				var personal = _repPersonal.CargarPersonalParaFiltro();

				return Ok(new { Personal = personal});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[Route("[Action]")]
		[HttpPost]
		public ActionResult CalculoChatsMessengerDiario([FromBody]ParametrosCalculoReporteChatDTO Fecha)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatMessengerRepositorio _repInteraccionChatMessenger = new InteraccionChatMessengerRepositorio(_integraDBContext);
				List<InteraccionChatMessengerBO> listaInteraccionChatMessenger = new List<InteraccionChatMessengerBO>();
				var interaccion = _repInteraccionChatMessenger.GetBy(x => x.FechaInicio.Date == Fecha.Fecha.Date).ToList();
				if(interaccion.Count == 0)
				{
					var chats = _repInteraccionChatMessenger.ObtenerCalculoChatMessengerDiario(Fecha.Fecha);

					var agrupado = chats.GroupBy(x => x.IdMessengerUsuario).Select(g => new DesagregadoMessengerDiarioDTO
					{
						IdMessengerUsuario = g.Key,
						DetalleMessengerUsuario = g.GroupBy(x => x.IdOportunidad).Select(x => new DesagregadoPorOportunidadMessengerDTO
						{
							IdOportunidad = x.Key,
							DetalleOportunidad = x.Select(y => new ChatMessengerDesagregadoPorMessengerUsuarioDiarioDTO
							{
								IdPersonal = y.IdPersonal,
								FechaChat = y.FechaChat,
								FechaInteraccion = y.FechaInteraccion,
								Mensaje = y.Mensaje,
								Remitente = y.Remitente,
								IdAlumno = y.IdAlumno,
								TipoOportunidad = y.TipoOportunidad

							}).ToList()
						}).ToList()
					}).ToList();

					foreach (var usuarioMessenger in agrupado)
					{
						InteraccionChatMessengerBO InteraccionChatMessengerBO = new InteraccionChatMessengerBO();
						InteraccionChatMessengerBO.IdMessengerUsuario = usuarioMessenger.IdMessengerUsuario;
						var remitente = "";
						DateTime fechaMensajeAnterior = DateTime.Now;
						DateTime fechaInicio = DateTime.Now;
						bool esPrimero = true;
						double suma = 0;
						int count = 0;
						int mensajesVisitante = 0;
						int? IdAlumno = null; // null
						int? idPersonal = null;
						int totalMensajesChat = 0;
						bool esAtendido = false;
						int countOpoSeg = 0;
						int countOpoGen = 0;

						bool primeroportunidad = true;
						foreach (var oportunidades in usuarioMessenger.DetalleMessengerUsuario)
						{
							var detalleOportunidad = oportunidades.DetalleOportunidad.OrderBy(x => x.FechaInteraccion).ToList();
							if (primeroportunidad)
							{
								foreach (var item in detalleOportunidad)
								{
									if (item.Remitente.Equals("Visitante"))
									{
										mensajesVisitante++;
									}
									if (esPrimero)
									{
										fechaInicio = item.FechaInteraccion;
										esPrimero = false;
									}
									else
									{
										if (item.Remitente.Equals("Asesor") && remitente.Equals("Visitante"))
										{
											TimeSpan diferencia = item.FechaInteraccion.Subtract(fechaMensajeAnterior);
											suma += diferencia.TotalSeconds;
											count++;
										}
									}
									IdAlumno = item.IdAlumno;
									idPersonal = item.IdPersonal;
									fechaMensajeAnterior = item.FechaInteraccion;
									remitente = item.Remitente;
								}
								if (detalleOportunidad.Where(x => x.Remitente.Equals("Asesor")).ToList().Count > 0)
								{
									esAtendido = true;
								}
								totalMensajesChat += detalleOportunidad.Count;

								primeroportunidad = false;
							}


							countOpoGen += detalleOportunidad.Where(x => x.TipoOportunidad == 1).ToList().Count();
							countOpoSeg += detalleOportunidad.Where(x => x.TipoOportunidad == 2).ToList().Count();
						}
						if (count == 0)
						{
							count = 1;
						}
						InteraccionChatMessengerBO.TieneOportunidadNueva = countOpoGen > 0 ? true : false;
						InteraccionChatMessengerBO.TieneOportunidadSeguimiento = countOpoSeg > 0 ? true : false;
						InteraccionChatMessengerBO.IdAlumno = IdAlumno;
						InteraccionChatMessengerBO.IdPersonal = idPersonal == null ? 0 : idPersonal.Value;
						InteraccionChatMessengerBO.EsAtendido = esAtendido;
						InteraccionChatMessengerBO.FechaInicio = fechaInicio;
						InteraccionChatMessengerBO.FechaFin = fechaMensajeAnterior;
						InteraccionChatMessengerBO.TotalMensajesVisitante = mensajesVisitante;
						InteraccionChatMessengerBO.TotalMensajesChat = totalMensajesChat;

						InteraccionChatMessengerBO.PromedioRespuestaUsuario = Convert.ToDecimal(suma / count);
						InteraccionChatMessengerBO.Estado = true;
						InteraccionChatMessengerBO.UsuarioCreacion = "SYSTEM";
						InteraccionChatMessengerBO.UsuarioModificacion = "SYSTEM";
						InteraccionChatMessengerBO.FechaCreacion = DateTime.Now;
						InteraccionChatMessengerBO.FechaModificacion = DateTime.Now;

						listaInteraccionChatMessenger.Add(InteraccionChatMessengerBO);
					}
					_repInteraccionChatMessenger.Insert(listaInteraccionChatMessenger);
				}
				
				return Ok(listaInteraccionChatMessenger);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[Route("[Action]")]
		[HttpPost]
		public ActionResult ReporteChatMessenger([FromBody]FiltrosChatMessengerReporteDTO data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatMessengerRepositorio _repInteraccionChatMessenger = new InteraccionChatMessengerRepositorio(_integraDBContext);
				var chats = _repInteraccionChatMessenger.obtenerReporteChatMessenger(data).OrderByDescending(x=>x.Fecha);
				IEnumerable<ReporteChatMessengerAgrupadoDTO> agrupado = null;
				if (data.Desglose == 1)
				{
					agrupado = chats.GroupBy(x => x.Fecha)
					.Select(g => new ReporteChatMessengerAgrupadoDTO
					{
						Fecha = g.Key.ToString("yyyyMMdd"),
						DetalleFecha = g.Select(y => new ReporteChatMessengerAgrupadoDetalleFechaDTO
						{
							Asesor = y.Asesor,
							NumeroChats = y.NumeroChats,
							NumeroChatsAtendidoOportunidadGenerada = y.NumeroChatsAtendidoOportunidadGenerada,
							NumeroChatsAtendidoOportunidadSeguimiento = y.NumeroChatsAtendidoOportunidadSeguimiento,
							NumeroChatsAtendidoSinOportunidadSeguimiento = y.NumeroChatsAtendidoSinOportunidadSeguimiento,
							NumeroOportunidadesGeneradas = y.NumeroOportunidadesGeneradas,
							NumeroOportunidadesSeguimiento = y.NumeroOportunidadesSeguimiento,
							PalabrasVisitante = y.PalabrasVisitante,
							PromedioRespuestaUsuario = y.PromedioRespuestaUsuario,
							NumeroChatsAtendidos = y.NumeroChatsAtendidos,
							NumeroChatsNuevosSinOportunidad = y.NumeroChatsNuevosSinOportunidad

						}).ToList()
					});
				}
				if (data.Desglose == 2)
				{
					agrupado = chats.GroupBy(x => _repInteraccionChatMessenger.ObtenerNumeroSemana(x.Fecha))
					.Select(g => new ReporteChatMessengerAgrupadoDTO
					{
						Fecha = "Semana_" + g.Key,
						DetalleFecha = g.Select(y => new ReporteChatMessengerAgrupadoDetalleFechaDTO
						{
							Asesor = y.Asesor,
							NumeroChats = y.NumeroChats,
							NumeroChatsAtendidoOportunidadGenerada = y.NumeroChatsAtendidoOportunidadGenerada,
							NumeroChatsAtendidoOportunidadSeguimiento = y.NumeroChatsAtendidoOportunidadSeguimiento,
							NumeroChatsAtendidoSinOportunidadSeguimiento = y.NumeroChatsAtendidoSinOportunidadSeguimiento,
							NumeroOportunidadesGeneradas = y.NumeroOportunidadesGeneradas,
							NumeroOportunidadesSeguimiento = y.NumeroOportunidadesSeguimiento,
							PalabrasVisitante = y.PalabrasVisitante,
							PromedioRespuestaUsuario = y.PromedioRespuestaUsuario,
							NumeroChatsAtendidos = y.NumeroChatsAtendidos,
							NumeroChatsNuevosSinOportunidad = y.NumeroChatsNuevosSinOportunidad

						}).ToList()
					});
				}
				if (data.Desglose == 3)
				{
					agrupado = chats.GroupBy(x => x.Fecha.Month)
					.Select(g => new ReporteChatMessengerAgrupadoDTO
					{
						Fecha = _repInteraccionChatMessenger.ObtenerNombreMes(g.Key),
						DetalleFecha = g.Select(y => new ReporteChatMessengerAgrupadoDetalleFechaDTO
						{
							Asesor = y.Asesor,
							NumeroChats = y.NumeroChats,
							NumeroChatsAtendidoOportunidadGenerada = y.NumeroChatsAtendidoOportunidadGenerada,
							NumeroChatsAtendidoOportunidadSeguimiento = y.NumeroChatsAtendidoOportunidadSeguimiento,
							NumeroChatsAtendidoSinOportunidadSeguimiento = y.NumeroChatsAtendidoSinOportunidadSeguimiento,
							NumeroOportunidadesGeneradas = y.NumeroOportunidadesGeneradas,
							NumeroOportunidadesSeguimiento = y.NumeroOportunidadesSeguimiento,
							PalabrasVisitante = y.PalabrasVisitante,
							PromedioRespuestaUsuario = y.PromedioRespuestaUsuario,
							NumeroChatsAtendidos = y.NumeroChatsAtendidos,
							NumeroChatsNuevosSinOportunidad = y.NumeroChatsNuevosSinOportunidad

						}).ToList()
					});
				}
				return Ok(agrupado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
